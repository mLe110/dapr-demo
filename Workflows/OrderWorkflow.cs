using Dapr.Workflow;
using Models;

namespace Workflows
{
    public class OrderWorkflow : Workflow<Order, Order>
    {
        public override async Task<Order> RunAsync(WorkflowContext context, Order order)
        {
            Console.WriteLine($"Order start: {order.OrderId}"); // Log for info purpose to see the behavior from the append log characteristic that is behind dapr workflow
            var updatedOrder = await context.CallActivityAsync<Order>(
                nameof(CreateDraftOrderActivity),
                order);
            Console.WriteLine($"Order before check: {order.OrderId}"); // Log for info purpose to see the behavior from the append log characteristic that is behind dapr workflow

            if (string.IsNullOrEmpty(updatedOrder.PartnerNumber))
            {
                updatedOrder = await context.CallActivityAsync<Order>(
                    nameof(CheckForDuplicateBusinessPartnerActivity),
                    updatedOrder);

                Console.WriteLine("Wait for partner update event");

                var partnerUpdate = await context.WaitForExternalEventAsync<PartnerUpdate>(
                    "partner-update-event",
                    TimeSpan.FromSeconds(60));

                updatedOrder = await context.CallActivityAsync<Order>(
                    nameof(UpdateOrderActivity),
                    new OrderWrapper
                    {
                        Order = updatedOrder,
                        PartnerUpdate = partnerUpdate
                    });
            }

            updatedOrder = await context.CallActivityAsync<Order>(
                nameof(UpdateOrderStatusActivity),
                updatedOrder);

            Console.WriteLine($"OrderWorkflow completed: {updatedOrder}");

            return updatedOrder;
        }
    }
}
using Dapr.Workflow;
using Models;

namespace Workflows
{
    public class UpdateOrderActivity : WorkflowActivity<OrderWrapper, Order>
    {
        private readonly ILogger<UpdateOrderActivity> _logger;

        public UpdateOrderActivity(ILogger<UpdateOrderActivity> logger)
        {
            _logger = logger;
        }

        public override Task<Order> RunAsync(WorkflowActivityContext context, OrderWrapper orderWrapper)
        {
            var order = orderWrapper.Order;
            order.PartnerNumber = orderWrapper.PartnerUpdate.SapPartnerNumber;

            _logger.LogInformation($"Updated Order {order.OrderId} with SAP Partner Number: {order.PartnerNumber}");

            return Task.FromResult(order);
        }
    }
}
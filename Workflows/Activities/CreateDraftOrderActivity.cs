using Dapr.Workflow;
using Models;

namespace Workflows
{
    public class CreateDraftOrderActivity : WorkflowActivity<Order, Order>
    {
        private readonly ILogger<CreateDraftOrderActivity> _logger;

        public CreateDraftOrderActivity(ILogger<CreateDraftOrderActivity> logger)
        {
            _logger = logger;
        }

        public override Task<Order> RunAsync(WorkflowActivityContext context, Order order)
        {
            order.Status = OrderStatus.DRAFT;

            _logger.LogInformation($"Order {order.OrderId} is now a draft.");

            return Task.FromResult(order);
        }
    }
}
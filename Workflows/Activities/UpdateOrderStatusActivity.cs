using Dapr.Workflow;
using Models;

namespace Workflows
{
    public class UpdateOrderStatusActivity : WorkflowActivity<Order, Order>
    {
        private readonly ILogger<UpdateOrderStatusActivity> _logger;

        public UpdateOrderStatusActivity(ILogger<UpdateOrderStatusActivity> logger)
        {
            _logger = logger;
        }

        public override Task<Order> RunAsync(WorkflowActivityContext context, Order order)
        {
            order.Status = OrderStatus.SUBMITTED;

            _logger.LogInformation($"Order {order.OrderId} is now submitted.");

            return Task.FromResult(order);
        }
    }
}
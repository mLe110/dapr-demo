using Dapr.Workflow;
using Models;

namespace Workflows
{
    public class CheckForDuplicateBusinessPartnerActivity : WorkflowActivity<Order, Order>
    {
        private readonly ILogger<CheckForDuplicateBusinessPartnerActivity> _logger;

        public CheckForDuplicateBusinessPartnerActivity(ILogger<CheckForDuplicateBusinessPartnerActivity> logger)
        {
            _logger = logger;
        }

        public override Task<Order> RunAsync(WorkflowActivityContext context, Order order)
        {
            _logger.LogInformation($"Checking for duplicate business partner for {order.FirstName} {order.LastName} - OrderId: {order.OrderId}");
            
            return Task.FromResult(order);
        }
    }
}
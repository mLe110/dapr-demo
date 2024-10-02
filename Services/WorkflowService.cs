using System.Threading.Tasks;
using Dapr.Client;
using Dapr.Workflow;
using Workflows;
using Models;

namespace Services
{
    public class WorkflowService
    {
        private readonly ILogger<WorkflowService> _logger;
        private readonly DaprWorkflowClient _workflowClient;

        public WorkflowService(ILogger<WorkflowService> logger, DaprWorkflowClient workflowClient)
        {
            _logger = logger;
            _workflowClient = workflowClient;
        }

        public string RunOrderWorkflow(string partnerNumber, string firstName, string lastName, string product)
        {
            string orderId = Guid.NewGuid().ToString()[..8];

            var order = new Order
            {
                OrderId = orderId,
                PartnerNumber = partnerNumber,
                FirstName = firstName,
                LastName = lastName,
                Product = product,
                Status = OrderStatus.NONE
            };

            _workflowClient.ScheduleNewWorkflowAsync(
                name: nameof(OrderWorkflow),
                instanceId: orderId,
                order);

            return orderId;
        }

        public async Task<string> MockPartnerUpdateEvent(string orderId)
        {
            var random = new Random();
            var partnerNumber = random.Next(1000000000, 2000000000).ToString(); // Ensures a 10-digit number

            _logger.LogInformation($"Generated Partner Number: {partnerNumber}");

            var partnerUpdate = new PartnerUpdate
            {
                OrderId = orderId,
                SapPartnerNumber = partnerNumber
            };

            await _workflowClient.RaiseEventAsync(
                instanceId: orderId,
                eventName: "partner-update-event",
                eventPayload: partnerUpdate);

            return partnerNumber;
        }   
    }
}
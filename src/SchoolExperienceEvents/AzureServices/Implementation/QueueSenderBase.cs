using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Polly.Registry;

namespace SchoolExperienceEvents.AzureServices.Implementation
{
    internal abstract class QueueSenderBase : QueueBase
    {
        public const string PolicyRegistryKey = "EventQueues";

        protected QueueSenderBase(string connectionString, IPolicyRegistry<string> policyRegistry, CancellationToken cancellationToken)
            : base(connectionString, policyRegistry, PolicyRegistryKey, cancellationToken)
        {
        }

        protected async Task AddMessageAsync<T>(string queueName, T eventData)
        {
            var json = JsonConvert.SerializeObject(eventData);
            var msg = new CloudQueueMessage(json);

            var queue = await GetQueueAsync(queueName).ConfigureAwait(false);
            await queue.AddMessageAsync(msg, null, null, null, null, CancellationToken).ConfigureAwait(false);
        }
    }
}

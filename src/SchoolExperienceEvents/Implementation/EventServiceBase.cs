using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;

namespace SchoolExperienceEvents.Implementation
{
    internal abstract class EventServiceBase
    {
        public const string PolicyRegistryKey = "EventQueues";
        private readonly ISyncPolicy<CloudQueue> _cachingPolicy;

        protected EventServiceBase(IOptions<EventServiceBaseOptions> options, IPolicyRegistry<string> policyRegistry)
        {
            StorageAccount = CloudStorageAccount.Parse(options.Value.ConnectionString);
            QueueClient = StorageAccount.CreateCloudQueueClient();

            _cachingPolicy = policyRegistry.Get<ISyncPolicy<CloudQueue>>(PolicyRegistryKey);
        }

        protected CloudQueueClient QueueClient { get; }
        protected CloudStorageAccount StorageAccount { get; }

        protected async Task AddMessageAsync<T>(string queueName, T eventData)
        {
            var json = JsonConvert.SerializeObject(eventData);
            var msg = new CloudQueueMessage(json);

            var queue = await GetQueueAsync(queueName).ConfigureAwait(false);
            await queue.AddMessageAsync(msg).ConfigureAwait(false);
        }

        private async Task<CloudQueue> GetQueueAsync(string name)
        {
            var queue = _cachingPolicy.Execute(context =>
            {
                var newQueue = QueueClient.GetQueueReference(name);
                newQueue.CreateIfNotExistsAsync().ConfigureAwait(false);

                return newQueue;
            }, new Context(name));

            return await Task.FromResult(queue);
        }
    }
}

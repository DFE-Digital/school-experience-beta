using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Polly;
using Polly.Registry;

namespace SchoolExperienceEvents.AzureServices.Implementation
{
    public abstract class QueueBase
    {
        private readonly ISyncPolicy<CloudQueue> _cachingPolicy;
        protected CloudQueueClient QueueClient { get; }
        protected CloudStorageAccount StorageAccount { get; }

        protected CancellationToken CancellationToken { get; }

        protected QueueBase(string connectionString, IPolicyRegistry<string> policyRegistry, string policyRegistryKey, CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            QueueClient = StorageAccount.CreateCloudQueueClient();

            _cachingPolicy = policyRegistry.Get<ISyncPolicy<CloudQueue>>(policyRegistryKey);
        }

        protected async Task<CloudQueue> GetQueueAsync(string name)
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

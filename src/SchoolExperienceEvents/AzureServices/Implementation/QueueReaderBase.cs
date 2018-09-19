using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Polly.Registry;

namespace SchoolExperienceEvents.AzureServices.Implementation
{
    public abstract class QueueReaderBase : QueueBase
    {
        private static readonly TimeSpan? MaximumWaitTime = TimeSpan.FromSeconds(10);

        protected QueueReaderBase(
            string connectionString, 
            IPolicyRegistry<string> policyRegistry, 
            string policyKey, 
            CancellationToken cancellationToken)
            : base(connectionString, policyRegistry, policyKey, cancellationToken)
        {
        }

        public async Task DeleteEvent(string queueName, CloudQueueMessage message)
        {
            var queue = await GetQueueAsync(queueName);

            await queue.DeleteMessageAsync(message);
        }

        public async Task<IEnumerable<CloudQueueMessage>> GetEvents<T>(string name, int maxMessages = 1)
            where T : class
        {
            var queue = await GetQueueAsync(name);

            return await queue.GetMessagesAsync(maxMessages, null, null, null, CancellationToken);
        }
    }
}

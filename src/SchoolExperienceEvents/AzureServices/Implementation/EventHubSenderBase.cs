using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace SchoolExperienceEvents.AzureServices.Implementation
{
    internal abstract class EventHubSenderBase
    {
        protected EventHubSenderBase(string hubName, string connectionString)
        {
            EventHubClient = OpenEventHub(connectionString, hubName);
        }

        protected EventHubClient EventHubClient { get; }

        protected async Task Send<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var bytes = Encoding.Default.GetBytes(json);
            await EventHubClient.SendAsync(new EventData(bytes));
        }

        private EventHubClient OpenEventHub(string connectionString, string hubName)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(connectionString)
            {
                EntityPath = hubName
            };
            return EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
    }
}

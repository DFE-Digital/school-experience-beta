using System;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SchoolExperienceNotificationProcessor
{
    internal class Program
    {
        private readonly string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=schoolexperiencebeta;AccountKey=jkZtrzupf+R8rpDk55mAXKOVCsMa/u3deEG5QxuaM8bAjmZSp+3/T5x5baUCchj76rE3cchZXN4PIgfBI9sGZA==;EndpointSuffix=core.windows.net";

        public Program()
        {
        }

        public CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainAsync();

            Console.ReadLine();
        }

        private static async void MainAsync()
        {
            var p = new Program();
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var queue = p.OpenQueue("notification");
            var message = await queue.GetMessageAsync(TimeSpan.FromSeconds(15),
                new QueueRequestOptions
                {
                    MaximumExecutionTime = TimeSpan.FromSeconds(10),
                },
                new OperationContext
                {
                },
                cancellationTokenSource.Token
            );
            if (message != null)
            {
                Console.WriteLine($"Get Message: {message.Id}");
                await queue.DeleteMessageAsync(message);

                Console.WriteLine($"Message Deleted");
            }
        }

        private CloudQueue OpenQueue(string queueName)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(StorageConnectionString);

            // Create a queue client for interacting with the queue service
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            Console.WriteLine("Open queue");

            CloudQueue queue = queueClient.GetQueueReference(queueName);
            return queue;
        }
    }
}

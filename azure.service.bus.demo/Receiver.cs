using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace azure.service.bus.demo
{
    public static class Receiver
    {
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Message received: {body}");

            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public static async Task ReceiveMessagesAsync(string serviceBusConnectionString, string queueOrTopicName,  string subscriptionName, bool useTopics)
        {
            await using ServiceBusClient client = new ServiceBusClient(serviceBusConnectionString);

            // create a processor that we can use to process the messages, either with a queue or a topic/subscription
            ServiceBusProcessor processor = useTopics
                ? client.CreateProcessor(queueOrTopicName, subscriptionName, new ServiceBusProcessorOptions { })
                : client.CreateProcessor(queueOrTopicName, new ServiceBusProcessorOptions());

            // add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();

            Console.WriteLine("Wait for a minute and then press any key to end the processing");
            Console.ReadKey();

            // stop processing 
            await processor.StopProcessingAsync();
        }
    }
}

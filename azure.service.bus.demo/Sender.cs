using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace azure.service.bus.demo
{
    public static class Sender
    {
        public static async Task SendMessagesAsync(string serviceBusConnectionString, string queueOrTopicName)
        {
            await using ServiceBusClient client = new ServiceBusClient(serviceBusConnectionString);
            ServiceBusSender sender = client.CreateSender(queueOrTopicName);

            for (int i = 1; i < 21; i++)
            {
                string message = $"Demo message {i}";
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);
                serviceBusMessage.ApplicationProperties.Add("MyMessageNumber", i);
                await sender.SendMessageAsync(serviceBusMessage);
                Console.WriteLine($"Sent: {message}");
            }
            
            Console.WriteLine("All message sent, press any key to receive the messages");
            Console.ReadKey();
        }
    }
}

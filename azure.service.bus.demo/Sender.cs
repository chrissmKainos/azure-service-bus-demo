using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace azure.service.bus.demo
{
    public static class Sender
    {
        public static async Task SendMessagesAsync(AppSettings appSettings)
        {
            await using ServiceBusClient client = new ServiceBusClient(appSettings.ServiceBusConnectionString);
            ServiceBusSender sender = client.CreateSender(appSettings.DemoQueueName);

            for (int i = 1; i < 21; i++)
            {
                string strMessage = $"Demo message {i}";
                ServiceBusMessage message = new ServiceBusMessage(strMessage);
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent: {strMessage}");
            }

            Console.WriteLine("All message sent to the queue, press any key to receive the messages");
            Console.ReadKey();
        }
    }
}

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace azure.service.bus.demo
{
    public class Program
    {
        static AppSettings appSettings = new AppSettings();

        static async Task Main(string[] args)
        {
            // get the configuration details from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            string queueOrTopicName = appSettings.UseTopic ? appSettings.DemoTopicName : appSettings.DemoQueueName;
            string subscriptionName = appSettings.UseTopic ? appSettings.DemoSubscriptionName : string.Empty;

            await Sender.SendMessagesAsync(appSettings.ServiceBusConnectionString, queueOrTopicName);
            await Receiver.ReceiveMessagesAsync(appSettings.ServiceBusConnectionString, queueOrTopicName, subscriptionName, appSettings.UseTopic);
        }
    }
}

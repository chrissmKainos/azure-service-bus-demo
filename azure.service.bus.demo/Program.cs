using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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

            // send messages to the queue
            await Sender.SendMessagesAsync(appSettings);

            // receive message from the queue
            await Receiver.ReceiveMessagesAsync(appSettings);
        }
    }
}

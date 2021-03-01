using System;
using System.Collections.Generic;
using System.Text;

namespace azure.service.bus.demo
{
    public class AppSettings
    {
        public bool UseTopic { get; set; }

        public String ServiceBusConnectionString { get; set; }

        public String DemoQueueName { get; set; }
        
        public String DemoTopicName { get; set; }

        public String DemoSubscriptionName { get; set; }
    }
}

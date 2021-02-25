using System;
using System.Collections.Generic;
using System.Text;

namespace azure.service.bus.demo
{
    public class AppSettings
    {
        public String ServiceBusConnectionString { get; set; }

        public String DemoQueueName { get; set; }
    }
}

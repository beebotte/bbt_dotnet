using Beebotte.API.Server.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Connector bbtConnector = new Connector("502b09f9113252ba91d0fa24b2e69c1e", "88303a6fdc866caeea2fe3bf4746611d16dce93196973d77e2037887f8fc6197", "api.demo.beebotte.com", "http", 80, "v1");
            AutoResetEvent timer = new AutoResetEvent(false);

            //Get all channels

            List<Channel> channels = bbtConnector.GetAllChannels();
            Reporter reporter = new Reporter(bbtConnector);

            TimerCallback tcb = reporter.Report;
            Timer stateTimer = new Timer(tcb, null, 1000, 5000);

            foreach (Channel c in channels)
            {
                Console.WriteLine(c.Name);
            }


            Console.Read();
        }
    }

    class Reporter
    {

        PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");
        PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        Connector bbtConnector;

        public Reporter(Connector c)
        {
            this.bbtConnector = c;
        }

        // This method is called by the timer delegate. 
        public void Report(Object c)
        {
            double mem = 100 - (100 * ramCounter.NextValue() / (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory));
            double cpu = cpuCounter.NextValue();
            Console.WriteLine(mem);
            Console.WriteLine(cpu);
            this.bbtConnector.Write("Win", "CPU", cpu);
            this.bbtConnector.Write("Win", "Memory", mem);
            Console.WriteLine();
        }
    }
}

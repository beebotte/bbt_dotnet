using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Beebotte.API.Server.Net;
using System.Configuration;

namespace Beebotte.Demo.Services
{
    public partial class ServerMonitoringService : ServiceBase
    {

        #region Fields

        private Connector bbtConnector;
        private object locker = new object();
        private Timer timer;
        private TotalCpuPerformanceCounter cpuPerformanceCounter;
        private AvailableMemoryPerformanceCounter memoryPerformanceCounter;
        private string channel;
        private string cpuResource;
        private string memoryResource;

        #endregion

        public ServerMonitoringService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartMonitor();
        }

        protected override void OnStop()
        {
            StopMonitor();
        }

        private void StartMonitor()
        {
            bbtConnector = new Connector(ConfigurationManager.AppSettings[ConfigurationKeys.AccessKey],
                                         ConfigurationManager.AppSettings[ConfigurationKeys.SecureKey]);
            channel = ConfigurationManager.AppSettings[ConfigurationKeys.Device];
            cpuResource = ConfigurationManager.AppSettings[ConfigurationKeys.CPUResource];
            memoryResource = ConfigurationManager.AppSettings[ConfigurationKeys.MemoryResource];
            cpuPerformanceCounter = new TotalCpuPerformanceCounter();
            memoryPerformanceCounter = new AvailableMemoryPerformanceCounter();
            this.timer = new Timer(new TimerCallback(Timer_Callback),
                                   null, 1000, 1000);
        }

        private void StopMonitor()
        {
            if (cpuPerformanceCounter != null)
            {
                cpuPerformanceCounter.Dispose();
                cpuPerformanceCounter = null;
            }

            if (memoryPerformanceCounter != null)
            {
                memoryPerformanceCounter.Dispose();
                memoryPerformanceCounter = null;
            }

            if (timer != null)
            {
                timer.Dispose();
            }
        }


        /// <summary>
        /// The method to be executed in the timer callback.
        /// </summary>
        private void Timer_Callback(object obj)
        {
            lock (locker)
            {
                var cpuUsage = cpuPerformanceCounter.GetUsage();
                var memoryUsage = memoryPerformanceCounter.GetUsage();
                //TODO check how to retrieve all cpu counters
                //TODO: remove hardcoded device desciptions
                bbtConnector.Write(channel, cpuResource, cpuUsage);

                bbtConnector.Write(channel, memoryResource, memoryUsage);
            }
        }
    }
}

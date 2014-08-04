using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Beebotte.Demo.Services
{

    public class TotalCpuPerformanceCounter : PerformanceCounterBase
    {
        private const string categoryName = "Processor";
        private const string counterName = "% Processor Time";
        private const string instanceName = "_Total";

        public TotalCpuPerformanceCounter()
            : base(categoryName, counterName, instanceName)
        {
        }
    }
}
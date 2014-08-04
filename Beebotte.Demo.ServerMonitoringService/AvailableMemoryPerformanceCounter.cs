using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Beebotte.Demo.Services
{

    public class AvailableMemoryPerformanceCounter : PerformanceCounterBase
    {
        private const string categoryName = "Memory";
        private const string counterName = "Available MBytes";

        public AvailableMemoryPerformanceCounter()
            : base(categoryName, counterName, String.Empty)
        {
        }
    }
}
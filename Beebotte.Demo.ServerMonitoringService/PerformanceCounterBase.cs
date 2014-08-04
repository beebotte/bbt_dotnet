using System;
using System.Diagnostics;


namespace Beebotte.Demo.Services
{
    public class PerformanceCounterBase : IDisposable
    {

        #region Fields

        private bool disposed = false;

        #endregion

        #region Properties

        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }
        public PerformanceCounter Counter { get; set; }

        #endregion

        #region ctor

        public PerformanceCounterBase(string category, string counter, string instance)
        {
            CategoryName = category;
            CounterName = counter;
            InstanceName = instance;
            Counter = String.IsNullOrEmpty(instance) ? new PerformanceCounter(category, counter) : new PerformanceCounter(category, counter, instance);
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            // Protect from being called multiple times.
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (Counter != null)
                {
                    Counter.Dispose();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Child class may override this method to determine whether the instance exists.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsInstanceExist()
        {
            return true;
        }

        public virtual double GetUsage()
        {
            if (!IsInstanceExist())
            {
                throw new ApplicationException(
                    string.Format("The instance {0} is not available. ",
                                  InstanceName));
            }

            return Counter.NextValue();
        }

        #endregion
    }
}

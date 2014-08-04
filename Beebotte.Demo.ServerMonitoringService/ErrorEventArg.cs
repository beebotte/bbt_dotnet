using System;

namespace Beebotte.Demo.Services
{

    public class ErrorEventArgs : EventArgs
    {
        public Exception Error { get; set; }
    }
}

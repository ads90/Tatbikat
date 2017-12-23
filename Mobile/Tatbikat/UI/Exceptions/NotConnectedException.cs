using System;
using System.Collections.Generic;
using System.Text;

namespace Tatbikat.UI.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException(string message) : base(message)
        {

        } 
    }
}

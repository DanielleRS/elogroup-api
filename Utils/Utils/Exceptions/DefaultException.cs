using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utils.Exceptions
{
    public class DefaultException : Exception
    {
        public int StatusCode { get; set; }
        public DefaultException(int code, string message) : base(message)
        {
            this.StatusCode = code;
        }
    }
}

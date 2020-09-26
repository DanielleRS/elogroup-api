using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Customers
{
    public class RegisterCustomerOutput
    {
        public string Method { get; set; }
        public string Result { get; set; }
        public CustomerDto Payload { get; set; }
    }
}

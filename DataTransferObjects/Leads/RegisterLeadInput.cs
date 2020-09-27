using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Leads
{
    public class RegisterLeadInput
    {

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string[] Opportunities { get; set; }
    }
}

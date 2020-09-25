using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Leads
{
    public class RegisterLeadOutput
    {
        public string Method { get; set; }
        public string Result { get; set; }
        public LeadDto Payload { get; set; }
    }
}

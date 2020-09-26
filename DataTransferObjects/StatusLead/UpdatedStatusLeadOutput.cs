using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.StatusLead
{
    public class UpdatedStatusLeadOutput
    {
        public string Method { get; set; }
        public string Result { get; set; }
        public int NewStatusId { get; set; }
    }
}

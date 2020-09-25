using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Lead
{
    public class LeadEntity
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }
        public int StatusId { get; set; }
    }
}

using DataTransferObjects.StatusLead;
using Entities.Lead;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Leads
{
    public class BasicLeadDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public StatusLeadDto Status { get; set; }
    }

    public static class BasicLeadDtoExtention
    {
        public static BasicLeadDto CreateBasicLeadDto(this LeadEntity input)
        {
            if (input == default)
                return default;

            return new BasicLeadDto()
            {
                Id = input.Id,
                CustomerName = input.CustomerName,
            };
        }
    }
}

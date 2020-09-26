using DataTransferObjects.Opportunities;
using DataTransferObjects.StatusLead;
using Entities.Lead;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Leads
{
    public class LeadDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public StatusLeadDto Status { get; set; }
        public List<OpportunityDto> Opportunities { get; set; }
    }

    public static class LeadDtoExtention
    {
        public static LeadDto CreateDto(this LeadEntity input)
        {
            if (input == default)
                return default;

            return new LeadDto()
            {
                Id = input.Id,
                Date = input.Date,
                CustomerName = input.CustomerName,
                CustomerPhone = input.CustomerPhone,
                CustomerEmail = input.CustomerEmail
            };
        }
    }
}

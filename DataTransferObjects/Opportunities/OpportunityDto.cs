using Entities.Oppotunity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Opportunities
{
    public class OpportunityDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public string Description { get; set; }
    }

    public static class OpportunityDtoExtention
    {
        public static OpportunityDto CreateDto(this OpportunityEntity input)
        {
            if (input == default)
                return default;

            return new OpportunityDto()
            {
                Id = input.Id,
                LeadId = input.LeadId,
                Description = input.Description
            };
        }
    }
}

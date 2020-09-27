using Entities.StautsLead;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.StatusLead
{
    public class StatusLeadDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public static class StatusLeadDtoExtention
    {
        public static StatusLeadDto CreateDto(this StatusLeadEntity input)
        {
            if (input == default)
                return default;

            return new StatusLeadDto()
            {
                Id = input.Id,
                Description = input.Description,
            };
        }
    }
}

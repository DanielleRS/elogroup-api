using DataTransferObjects.Leads;
using DataTransferObjects.Opportunities;
using Entities.Lead;
using Entities.Oppotunity;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LeadServices
{
    public class RegisterLeadService : IRegisterLeadService
    {
        private readonly ILeadRepository _leadRepository;
        public RegisterLeadService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }
        public async Task<RegisterLeadOutput> RegisterLead(RegisterLeadInput request)
        {
            LeadEntity input = new LeadEntity()
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                CustomerPhone = request.CustomerPhone,
                StatusId = 1 //buscar status no banco: cliente potencial
            };

            var lead = await _leadRepository.AddLead(input);

            foreach (var item in request.Opportunities)
            {
                var opportunity = new OpportunityEntity()
                {
                    LeadId = lead.Id,
                    Description = item
                };
                await _leadRepository.AddOpportunities(opportunity);
            }

            var opportunitiesOfLead = await _leadRepository.ListOpportunitiesByLeadId(lead.Id);

            var leadDto = lead.CreateDto();
            leadDto.Opportunities = new List<OpportunityDto>();
            opportunitiesOfLead.ToList().ForEach(item => leadDto.Opportunities.Add(item.CreateDto()));

            return new RegisterLeadOutput()
            {
                Method = "RegisterLead",
                Result = lead != default ? "SUCCESS" : "ERROR",
                Payload = leadDto
            };
        }
    }
}

using DataTransferObjects.Leads;
using DataTransferObjects.Opportunities;
using Entities.Lead;
using Entities.Oppotunity;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;
using Utils.Utils.Enums;

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
            var status = await _leadRepository.AddStatus(request.StatusDescription);
            //var allStatusLead = await _leadRepository.ListAllStatusLead();
            //if (!allStatusLead.Any())
                //throw new DefaultException((int)HttpStatusCode.InternalServerError, "Nenhum status lead encontrado.");

            LeadEntity input = new LeadEntity()
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                CustomerPhone = request.CustomerPhone,
                StatusId = status.Id
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

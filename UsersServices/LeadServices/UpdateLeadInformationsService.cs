using DataTransferObjects.StatusLead;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LeadServices
{
    public class UpdateLeadInformationsService : IUpdateLeadInformationsService
    {
        private readonly ILeadRepository _leadRepository;
        public UpdateLeadInformationsService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }
        public async Task<UpdatedStatusLeadOutput> UpdateLeadInformations(int leadId, int newStatusId, DateTime date)
        {
            var allOpportunities = await _leadRepository.ListOpportunitiesByLeadId(leadId);
            foreach (var opportunity in allOpportunities)
            {
                opportunity.Description += $" [Agendado: {date.Day}/{date.Month}/{date.Year} {date.Hour}:{date.Minute}]";
                await _leadRepository.UpdateOpportunityDescriptionByLeadId(leadId, opportunity.Id, opportunity.Description);
            }

            await _leadRepository.UpdateLeadStatus(leadId, newStatusId);
            
            return new UpdatedStatusLeadOutput()
            {
                Method = "UpdateStatusLead",
                Result = "SUCCESS",
                NewStatusId = newStatusId
            };
        }
    }
}

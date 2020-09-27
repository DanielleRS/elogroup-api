using DataTransferObjects.StatusLead;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.LeadServices
{
    public class GetStatusByDescriptionService : IStatusByDescriptionService
    {
        private readonly ILeadRepository _leadRepository;
        public GetStatusByDescriptionService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }
        public async Task<StatusLeadOutput> GetStatusByDescription(string description)
        {
            var statusLead = await _leadRepository.GetStatusByDescription(description);
            var statusDto = statusLead.CreateDto();
            
            return new StatusLeadOutput()
            {
                Status = statusDto

            };
        }
    }
}

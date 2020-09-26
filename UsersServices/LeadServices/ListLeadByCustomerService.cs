using DataTransferObjects.Leads;
using DataTransferObjects.Opportunities;
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
    public class ListLeadByCustomerService : IListLeadByCustomerService
    {
        private readonly ILeadRepository _leadRepository;
        public ListLeadByCustomerService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }
        public async Task<GetLeadOutput> ListLeadByCustomer()
        {
            var allUserLeads = await _leadRepository.ListLeadsByCustomer();

            List<BasicLeadDto> leadDto = new List<BasicLeadDto>();
            foreach (var leadEntity in allUserLeads)
            {
                var dto = leadEntity.CreateBasicLeadDto();
                var statusEntity = await _leadRepository.GetStatusById(leadEntity.StatusId);
                dto.Status = statusEntity.CreateDto();
                leadDto.Add(dto);
            }

            return new GetLeadOutput
            {
                Leads = leadDto
            };
        }
    }
}

using DataTransferObjects.Leads;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LeadServices
{
    public class ListAllLeadsService : IListAllLeadsService
    {
        private readonly ILeadRepository _leadRepository;
        public ListAllLeadsService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }
        public async Task<ListAllLeadsOutput> ListAllLeads()
        {
            var allLeads = await _leadRepository.ListAllLeads();
            var listLeadDto = new List<LeadDto>();
            allLeads.ToList().ForEach(leadEntity =>
            {
                listLeadDto.Add(leadEntity.CreateDto());
            });

            return new ListAllLeadsOutput()
            {
                Leads = listLeadDto
            };
        }
    }
}

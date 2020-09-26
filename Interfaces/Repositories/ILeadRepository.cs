using Entities.Lead;
using Entities.Oppotunity;
using Entities.StautsLead;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface ILeadRepository
    {
        Task<LeadEntity> AddLead(LeadEntity lead);

        Task<int> AddOpportunities(OpportunityEntity opportunity);

        Task<IEnumerable<OpportunityEntity>> ListOpportunitiesByLeadId(int leadId);

        Task<IEnumerable<OpportunityEntity>> ListAllOpportunities();

        Task<IEnumerable<StatusLeadEntity>> ListAllStatusLead();

        Task<IEnumerable<LeadEntity>> ListLeadsByCustomer();

        Task<StatusLeadEntity> GetStatusById(int id);
        Task<IEnumerable<LeadEntity>> ListAllLeads();
    }
}

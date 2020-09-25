using Entities.Lead;
using Entities.Oppotunity;
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
    }
}

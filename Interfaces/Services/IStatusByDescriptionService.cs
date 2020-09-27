using DataTransferObjects.Leads;
using DataTransferObjects.StatusLead;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IStatusByDescriptionService
    {
        Task<StatusLeadOutput> GetStatusByDescription(string description);
    }
}

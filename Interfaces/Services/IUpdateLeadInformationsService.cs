using DataTransferObjects.StatusLead;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IUpdateLeadInformationsService
    {
        Task<UpdatedStatusLeadOutput> UpdateLeadInformations(int leadId, DateTime date, string statusDescription);
    }
}

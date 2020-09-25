using DataTransferObjects.Leads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IRegisterLeadService
    {
        Task<RegisterLeadOutput> RegisterLead(RegisterLeadInput request);
    }
}

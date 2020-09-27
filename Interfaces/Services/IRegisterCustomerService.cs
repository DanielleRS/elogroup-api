using DataTransferObjects.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IRegisterCustomerService
    {
        Task<RegisterCustomerOutput> RegisterCustomer(int leadId);
    }
}

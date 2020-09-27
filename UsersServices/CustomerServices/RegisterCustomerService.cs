using DataTransferObjects.Customers;
using Entities.Customer;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomerServices
{
    public class RegisterCustomerService : IRegisterCustomerService
    {
        private readonly ILeadRepository _leadRepository;
        public RegisterCustomerService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }
        public async Task<RegisterCustomerOutput> RegisterCustomer(int leadId, string statusDescription)
        {
            CustomerEntity input = new CustomerEntity()
            {
                LeadId = leadId
            };

            var customer = await _leadRepository.AddCustomer(input);
            var status = await _leadRepository.AddStatus(statusDescription);

            //var statusConfirmedData = status.FirstOrDefault(s => s.Description == "Dados Confirmados");
            await _leadRepository.UpdateLeadStatus(leadId, status.Id);

            return new RegisterCustomerOutput()
            {
                Method = "RegisterCustomer",
                Result = customer != default ? "SUCCESS" : "ERROR",
                Payload = customer.CreateDto()
            };
        }
    }
}

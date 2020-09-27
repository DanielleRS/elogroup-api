using Entities.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
    }

    public static class CustomerDtoExtention
    {
        public static CustomerDto CreateDto(this CustomerEntity input)
        {
            if (input == default)
                return default;

            return new CustomerDto()
            {
                Id = input.Id,
                LeadId = input.LeadId
            };
        }
    }
}

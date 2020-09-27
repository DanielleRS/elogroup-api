﻿using DataTransferObjects.Leads;
using DataTransferObjects.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IListLeadByCustomerService
    {
        Task<GetLeadOutput> ListLeadByCustomer();
    }
}

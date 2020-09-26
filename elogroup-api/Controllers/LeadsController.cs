using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTransferObjects.Leads;
using DataTransferObjects.Opportunities;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Utils.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace elogroup_api.Controllers
{
    [Route("api/leads")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly IRegisterLeadService _registerLeadService;
        private readonly IListLeadByCustomerService _listLeadByUserService;
        private readonly IListAllLeadsService _listAllLeadsService;

        public LeadsController(
            IRegisterLeadService registerLeadService, 
            IListLeadByCustomerService listAllOpportunitiesService,
            IListAllLeadsService listAllLeadsService
        )
        {
            this._registerLeadService = registerLeadService;
            this._listLeadByUserService = listAllOpportunitiesService;
            this._listAllLeadsService = listAllLeadsService;
        }

        // POST api/<LeadsController>
        [HttpPost]
        public async Task<ActionResult<string>> AddLead([FromBody] RegisterLeadInput registerLeadInput)
        {
            try
            {
                var result = await _registerLeadService.RegisterLead(registerLeadInput);
                return StatusCode((int)HttpStatusCode.Created, result);
            }
            catch (DefaultException e)
            {
                throw new DefaultException(e.StatusCode, e.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("customer")]
        public async Task<ActionResult<List<OpportunityDto>>> ListLeadsByCustomer()
        {
            try
            {
                var result = await _listLeadByUserService.ListLeadByCustomer();
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (DefaultException e)
            {
                throw new DefaultException(e.StatusCode, e.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("")]
        public async Task<ActionResult<List<OpportunityDto>>> ListAllLeads()
        {
            try
            {
                var result = await _listAllLeadsService.ListAllLeads();
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (DefaultException e)
            {
                throw new DefaultException(e.StatusCode, e.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTransferObjects.Customers;
using DataTransferObjects.Leads;
using DataTransferObjects.Opportunities;
using DataTransferObjects.StatusLead;
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
        private readonly IRegisterCustomerService _registerCustomerService;
        private readonly IUpdateLeadInformationsService _updateStatusLeadService;
        private readonly IStatusByDescriptionService _statusByDescriptionService;

        public LeadsController(
            IRegisterLeadService registerLeadService,
            IListLeadByCustomerService listAllOpportunitiesService,
            IListAllLeadsService listAllLeadsService,
            IRegisterCustomerService registerCustomerService, 
            IUpdateLeadInformationsService updateStatusLeadService,
            IStatusByDescriptionService statusByDescriptionService)
        {
            this._registerLeadService = registerLeadService;
            this._listLeadByUserService = listAllOpportunitiesService;
            this._listAllLeadsService = listAllLeadsService;
            this._registerCustomerService = registerCustomerService;
            this._updateStatusLeadService = updateStatusLeadService;
            this._statusByDescriptionService = statusByDescriptionService;
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

        [HttpGet("status/{description}")]
        public async Task<ActionResult<string>> GetStatusByDescription([FromRoute] string description)
        {
            try
            {
                var result = await _statusByDescriptionService.GetStatusByDescription(description);
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

        [HttpPost("{leadId}/customer")]
        public async Task<ActionResult<string>> AddCustomer([FromRoute] string leadId)
        {
            try
            {
                if (!Int32.TryParse(leadId, out int intLeadId))
                    throw new DefaultException((int)HttpStatusCode.BadRequest, "Parâmetro inválido");

                var result = await _registerCustomerService.RegisterCustomer(intLeadId);
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

        [HttpPut("{leadId}/status/{statusId}")]
        public async Task<ActionResult<UpdatedStatusLeadOutput>> UpdateLeadInformations([FromRoute] string leadId, [FromRoute] string statusId, [FromBody] LeadInformationsInput date)
        {
            try
            {
                if (!Int32.TryParse(leadId, out int intLeadId) || !Int32.TryParse(statusId, out int intStatusId))
                    throw new DefaultException((int)HttpStatusCode.BadRequest, "Parâmetro inválido");

                var result = await _updateStatusLeadService.UpdateLeadInformations(intLeadId, intStatusId, date.Date);
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

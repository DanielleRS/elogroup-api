using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTransferObjects.Leads;
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

        public LeadsController(IRegisterLeadService registerLeadService)
        {
            this._registerLeadService = registerLeadService;
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
    }
}

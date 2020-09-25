using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTransferObjects.Users;
using Utils.Exceptions;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace elogroup_api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRegisterUserService _registerUserService;
        public UsersController(IRegisterUserService registerUserService)
        {
            this._registerUserService = registerUserService;
        }

        [HttpPost()]
        public async Task<ActionResult<string>> AddUser([FromBody] RegisterUserInput registerUserInput)
        {
            try
            {
                var result = await _registerUserService.Register(registerUserInput);
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

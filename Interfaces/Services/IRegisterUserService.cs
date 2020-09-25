using DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IRegisterUserService
    {
        Task<RegisterUserOutput> Register(RegisterUserInput request);
    }
}

using DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IRegisterUserService
    {
        string Register(RegisterUserInput request);
    }
}

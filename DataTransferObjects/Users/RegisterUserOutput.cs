using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Users
{
    public class RegisterUserOutput
    {
        public string Method { get; set; }
        public string Result { get; set; }
        public UserDto Payload { get; set; }
    }
}

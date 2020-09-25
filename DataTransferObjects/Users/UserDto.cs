using Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public static class BaseClientDtoExtention
    {
        public static UserDto CreateDto(this UserEntity input)
        {
            if (input == default)
                return default;

            return new UserDto()
            {
                Id = input.Id,
                Password = input.Password,
                UserName = input.UserName
            };
        }
    }
}

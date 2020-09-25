using DataTransferObjects.Users;
using Entities.User;
using Interfaces.Repositories;
using Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IUserRepository _userRepository;
        public RegisterUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<RegisterUserOutput> Register(RegisterUserInput request)
        {
            UserEntity input = new UserEntity()
            {
                UserName = request.UserName,
                Password = request.Password
            };

            var userId = await _userRepository.Add(input);

            return new RegisterUserOutput()
            {
                Id = userId
            };
        }
    }
}

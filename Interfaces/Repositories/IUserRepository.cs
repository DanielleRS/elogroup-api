using Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(UserEntity user);
    }
}

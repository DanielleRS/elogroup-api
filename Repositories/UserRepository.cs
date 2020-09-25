using Dapper;
using Entities.User;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : SqlBaseConnection<UserEntity>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {

        }
        public async Task<int> Add(UserEntity user)
        {
            DynamicParameters params = new DynamicParameters();
            string sqlQuery = @"
                INSERT INTO users(user_name, password) values(@userName, @password)
            ";

            var id = await base.ExecutarAsync(sqlQuery, params);
            return id;
        }
    }
}

using Dapper;
using Entities.User;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Repositories
{
    public class UserRepository : SqlBaseConnection<UserEntity>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {

        }
        public async Task<int> Add(UserEntity user)
        {
            string sqlInsertQuery = @"
                INSERT INTO [dbo].[User]([UserName], [Password]) values(@userName, @password)
            ";

            string sqlSelectQuery = @"
                SELECT 
                    [Id], 
                    [UserName],
                    [Password]
                FROM [dbo].[User]
                WHERE UserName = @userName AND Password = @password
            ";

            DynamicParameters insertParameters = new DynamicParameters();
            insertParameters.Add("@userName", user.UserName, DbType.AnsiString);
            insertParameters.Add("@password", user.Password, DbType.AnsiString);

            var id = await ExecutarAsync(sqlInsertQuery, insertParameters);

            if(id == 0)
                throw new DefaultException(500, "Erro no banco de dados.");
            
            DynamicParameters selectParameters = new DynamicParameters();
            selectParameters.Add("@userName", user.UserName, DbType.AnsiString);
            selectParameters.Add("@password", user.Password, DbType.AnsiString);

            var newUser = await ObterAsync<UserEntity>(sqlSelectQuery, selectParameters);

            return newUser.Id;
        }
    }
}

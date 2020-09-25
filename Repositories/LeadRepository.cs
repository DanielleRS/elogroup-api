using Dapper;
using Entities.Lead;
using Entities.Oppotunity;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Repositories
{
    public class LeadRepository : SqlBaseConnection<LeadEntity>, ILeadRepository
    {
        public LeadRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<LeadEntity> AddLead(LeadEntity lead)
        {
            string sqlInsertQuery = @"
                INSERT INTO [dbo].[Lead]([Date], [CustomerName], [CustomerPhone], [CustomerEmail], [StatusId]) 
                    values(@date, @customerName, @customerPhone, @customerEmail, @statusId)
            ";

            string sqlSelectQuery = @"
                SELECT 
                    [Id], 
                    [Date],
                    [CustomerName],
                    [CustomerPhone],
                    [CustomerEmail], 
                    [StatusId]
                FROM [dbo].[Lead]
                WHERE CustomerName = @CustomerName AND CustomerEmail = @CustomerEmail
            ";

            DynamicParameters insertParameters = new DynamicParameters();
            insertParameters.Add("@date", DateTime.Now, DbType.DateTime);
            insertParameters.Add("@customerName", lead.CustomerName, DbType.AnsiString);
            insertParameters.Add("@customerPhone", lead.CustomerPhone, DbType.AnsiString);
            insertParameters.Add("@customerEmail", lead.CustomerEmail, DbType.AnsiString);
            insertParameters.Add("@statusId", lead.StatusId, DbType.Int32);

            var insertedRows = await ExecutarAsync(sqlInsertQuery, insertParameters);

            if (insertedRows == 0)
                throw new DefaultException(500, "Erro no banco de dados.");

            DynamicParameters selectParameters = new DynamicParameters();
            selectParameters.Add("@customerName", lead.CustomerName, DbType.AnsiString);
            selectParameters.Add("@customerEmail", lead.CustomerEmail, DbType.AnsiString);

            return await ObterAsync<LeadEntity>(sqlSelectQuery, selectParameters);
        }

        public async Task<int> AddOpportunities(OpportunityEntity opportunity)
        {
            string sqlInsertQuery = @"
                INSERT INTO [dbo].[Opportunity]([LeadId], [Description]) 
                    values(@leadId, @description)
            ";

            DynamicParameters insertParameters = new DynamicParameters();
            insertParameters.Add("@leadId", opportunity.LeadId, DbType.Int32);
            insertParameters.Add("@description", opportunity.Description, DbType.AnsiString);

            return await ExecutarAsync(sqlInsertQuery, insertParameters);
        }

        public async Task<IEnumerable<OpportunityEntity>> ListOpportunitiesByLeadId(int leadId)
        {
            string sqlSelectQuery = @"
                SELECT 
                    [Id], 
                    [LeadId],
                    [Description]
                FROM [dbo].[Opportunity]
                WHERE LeadId = @leadId
            ";
            DynamicParameters selectParameters = new DynamicParameters();
            selectParameters.Add("@leadId", leadId, DbType.Int32);

            return await ListarAsync<OpportunityEntity>(sqlSelectQuery, selectParameters);
        }
    }
}

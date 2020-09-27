using Dapper;
using DataTransferObjects.StatusLead;
using Entities.Customer;
using Entities.Lead;
using Entities.Oppotunity;
using Entities.StautsLead;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<IEnumerable<StatusLeadEntity>> ListAllStatusLead()
        {
            string sqlSelectQuery = @"
                SELECT 
                    [Id],
                    [Description]
                FROM [dbo].[StatusLead]
            ";

            return await ListarAsync<StatusLeadEntity>(sqlSelectQuery, null);
        }

        public async Task<IEnumerable<OpportunityEntity>> ListAllOpportunities()
        {
            string sqlSelectQuery = @"
                SELECT DISTINCT
                    [Id],
                    [LeadId],
                    [Description]
                FROM [dbo].[Opportunity]
            ";

            return await ListarAsync<OpportunityEntity>(sqlSelectQuery, null);
        }

        public async Task<IEnumerable<LeadEntity>> ListLeadsByCustomer()
        {
            string sqlSelectQuery = @"
                SELECT
                    l.[Id], 
                    l.[Date],
                    l.[CustomerName],
                    l.[CustomerPhone],
                    l.[CustomerEmail], 
                    l.[StatusId]
                FROM [dbo].[Lead] AS l
                INNER JOIN [dbo].[Customer] AS c ON l.Id = c.LeadId
            ";

            return await ListarAsync<LeadEntity>(sqlSelectQuery, null);
        }

        public async Task<IEnumerable<LeadEntity>> ListAllLeads()
        {
            string sqlSelectQuery = @"
                SELECT
                    l.[Id], 
                    l.[Date],
                    l.[CustomerName],
                    l.[CustomerPhone],
                    l.[CustomerEmail], 
                    l.[StatusId]
                FROM [dbo].[Lead] AS l
            ";

            return await ListarAsync<LeadEntity>(sqlSelectQuery, null);
        }

        public async Task<StatusLeadEntity> GetStatusById(int id)
        {
            string sqlSelectQuery = @"
                SELECT 
                    [Id],
                    [Description]
                FROM [dbo].[StatusLead]
                WHERE Id = @id
            ";

            DynamicParameters selectParameters = new DynamicParameters();
            selectParameters.Add("@id", id, DbType.Int32);

            return await ObterAsync<StatusLeadEntity>(sqlSelectQuery, selectParameters);
        }

        public async Task<StatusLeadEntity> GetStatusByDescription(string description)
        {
            string sqlSelectQuery = @"
                SELECT 
                    [Id],
                    [Description]
                FROM [dbo].[StatusLead]
                WHERE Description = @description
            ";

            DynamicParameters selectParameters = new DynamicParameters();
            selectParameters.Add("@description", description, DbType.AnsiString);

            return await ObterAsync<StatusLeadEntity>(sqlSelectQuery, selectParameters);
        }

        public async Task<StatusLeadEntity> AddStatus(string statusDescription)
        {
            string sqlInsertQuery = @"
                INSERT INTO [dbo].[StatusLead]([Description]) values(@description)
            ";

            string sqlSelectQuery = @"
                SELECT 
                    [Id], 
                    [Description]
                FROM [dbo].[StatusLead]
                WHERE Description = @description
            ";

            StatusLeadEntity correctStatus;

            DynamicParameters selectParameters = new DynamicParameters();
            selectParameters.Add("@description", statusDescription, DbType.AnsiString);

            var result = await ListarAsync<StatusLeadEntity>(sqlSelectQuery, selectParameters);
            correctStatus = result.FirstOrDefault(s => s.Description == statusDescription);

            if (correctStatus != default)
                return correctStatus;

            DynamicParameters insertParameters = new DynamicParameters();
            insertParameters.Add("@description", statusDescription, DbType.AnsiString);

            var insertedRows = await ExecutarAsync(sqlInsertQuery, insertParameters);

            if (insertedRows == 0)
                throw new DefaultException(500, "Erro no banco de dados.");

            return await ObterAsync<StatusLeadEntity>(sqlSelectQuery, selectParameters);
        }
        public async Task<CustomerEntity> AddCustomer(CustomerEntity customer)
        {
            string sqlInsertQuery = @"
                INSERT INTO [dbo].[Customer]([LeadId]) values(@leadId)
            ";

            string sqlSelectQuery = @"
                SELECT 
                    [Id], 
                    [LeadId]
                FROM [dbo].[Customer]
                WHERE LeadId = @leadId
            ";

            DynamicParameters insertParameters = new DynamicParameters();
            insertParameters.Add("@leadId", customer.LeadId, DbType.AnsiString);

            var insertedRows = await ExecutarAsync(sqlInsertQuery, insertParameters);

            if (insertedRows == 0)
                throw new DefaultException(500, "Erro no banco de dados.");

            DynamicParameters selectParameters = new DynamicParameters();
            selectParameters.Add("@leadId", customer.LeadId, DbType.Int32);

            return await ObterAsync<CustomerEntity>(sqlSelectQuery, selectParameters);
        }

        public async Task<int> UpdateLeadStatus(int leadId, int newStatusLead)
        {
            string updateSqlQuery = @"
                UPDATE [dbo].[Lead] 
                SET [StatusId] = @newStatusLead
                WHERE [Id] = @leadId
            ";

            DynamicParameters updateParameters = new DynamicParameters();
            updateParameters.Add("@newStatusLead", newStatusLead, DbType.Int32);
            updateParameters.Add("@leadId", leadId, DbType.Int32);

            return await ExecutarAsync(updateSqlQuery, updateParameters);
        }
        public async Task<int> UpdateOpportunityDescriptionByLeadId(int leadId, int newOpportunityId, string newOpportunityDescription)
        {
            string updateSqlQuery = @"
                UPDATE [dbo].[Opportunity] 
                SET [Description] = @newOpportunityDescription
                WHERE [Id] = @newOpportunityId AND [LeadId] = @leadId
            ";

            DynamicParameters updateParameters = new DynamicParameters();
            updateParameters.Add("@leadId", leadId, DbType.Int32);
            updateParameters.Add("@newOpportunityId", newOpportunityId, DbType.Int32);
            updateParameters.Add("@newOpportunityDescription", newOpportunityDescription, DbType.AnsiString);

            return await ExecutarAsync(updateSqlQuery, updateParameters);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public abstract class SqlBaseConnection<TEntidade>
	{
		protected readonly string connectionString;

		protected SqlBaseConnection(string connectionString)
		{
			this.connectionString = connectionString;
		}

		protected IDbConnection ObterConexao()
		{
			return new SqlConnection(connectionString);
		}

		protected async Task<int> ExecutarAsync(string sql, object parametros)
		{
			using (var conexao = ObterConexao())
			{
				return await conexao.ExecuteAsync(sql, parametros);
			}
		}

		protected async Task<IEnumerable<T>> ListarAsync<T>(string sql, object parametros)
		{
			using (var conexao = ObterConexao())
			{
				return await conexao.QueryAsync<T>(sql, parametros);
			}
		}

		protected async Task<T> ObterAsync<T>(string sql, object parametros)
		{
			using (var conexao = ObterConexao())
			{
				return await conexao.QueryFirstOrDefaultAsync<T>(sql, parametros);
			}
		}

		protected IEnumerable<TEntidade> Listar(string sql, object parametros)
		{
			using (var conexao = ObterConexao())
			{
				return conexao.Query<TEntidade>(sql, parametros);
			}
		}

		protected IEnumerable<T> Listar<T>(string sql, object parametros)
		{
			using (var conexao = ObterConexao())
			{
				return conexao.Query<T>(sql, parametros);
			}
		}

		protected T Obter<T>(string sql, object parametros)
		{
			using (var conexao = ObterConexao())
			{
				return conexao.QueryFirstOrDefault<T>(sql, parametros);
			}
		}
	}
}

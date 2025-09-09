using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.SqlClient;

namespace MyApp.Infrastructure.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _postgresConnectionString;
        //private readonly string _sqlServerConnectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _postgresConnectionString = _configuration.GetConnectionString("PostgresConnection");
            //_sqlServerConnectionString = _configuration.GetConnectionString("SqlServerConnection");
        }

        public IDbConnection CreatePostgresConnection()
            => new NpgsqlConnection(_postgresConnectionString);

        //public IDbConnection CreateSqlServerConnection()
        //    => new SqlConnection(_sqlServerConnectionString);
    }
}

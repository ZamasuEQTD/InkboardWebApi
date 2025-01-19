using System.Data;
using Application.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infraestructure.Persistence
{
    public class NpgsqlConnectionFactory : IDBConnectionFactory
    {
        private readonly string _connectionString;
        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
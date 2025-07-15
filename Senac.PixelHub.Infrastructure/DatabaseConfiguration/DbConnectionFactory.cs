using System.Data;
using Microsoft.Data.SqlClient;

namespace Senac.PixelHub.Infrastructure.DatabaseConfiguration
{
    public class DbConnectionFactory : IDbConnectionFactory
    {

        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}

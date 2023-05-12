using System.Data;
using Microsoft.Data.SqlClient;
using Sofka.ProductInventory.Core.Domain.Interfaces;

namespace Sofka.ProductInventory.Infrastucture.Data
{
    public class DbConnectionFactory:IDbConnectionFactory
    {
        private readonly string _connectionString;
        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection( _connectionString );
        }
    }
}

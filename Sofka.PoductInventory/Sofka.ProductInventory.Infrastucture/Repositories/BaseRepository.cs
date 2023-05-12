using Dapper;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using System.Data;


namespace Sofka.ProductInventory.Infrastucture.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public BaseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task AddAsync(T entity)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var query = "INSERT INTO " + typeof(T).Name + " VALUES (@Property1, @Property2)";
            await connection.ExecuteAsync(query,entity);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

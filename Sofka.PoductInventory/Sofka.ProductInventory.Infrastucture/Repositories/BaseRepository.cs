using Dapper;
using Dapper.Contrib.Extensions;
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
            var typeName = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            //cada item de properties tiene la propeidad Name, la idea es recorrer ese array y concatenar, tener en cuenta el siguiente código
            /*
             *   var sql = string.Format("INSERT INTO [{0}] ({1}) 
            VALUES (@{2}) SELECT CAST(scope_identity() AS int)",
                typeof(T).Name,
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(", @", propertyContainer.ValueNames));
             */

            using var connection = _dbConnectionFactory.CreateConnection();
            var query = "INSERT INTO " + typeof(T).Name + " VALUES (@Id,@Name, @InInventory,@Min,@Max,@BuyId)";
            //await connection.ExecuteAsync(query,entity);
            await connection.InsertAsync<T>(entity);
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

using Dapper;
using Dapper.Contrib.Extensions;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;


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
            //var query = "INSERT INTO " + typeof(T).Name + "(Name,InInventory,Min,Max) VALUES (@Name, @InInventory,@Min,@Max)";
            //Product product = new Product { InInventory = 1, Max = 500, Min = 200, Name = "agua" };
            //await connection.ExecuteAsync(query, product);
            await connection.InsertAsync<T>(entity);
            //dapper contrib
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}");
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

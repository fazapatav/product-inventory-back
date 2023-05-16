using Dapper;
using Sofka.ProductInventory.Core.Domain.Interfaces;


namespace Sofka.ProductInventory.Infrastucture.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public BaseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<int> AddAsync(T entity)
        {
            dynamic properties = typeof(T).GetProperties();
            List<string> entityProperties = GetEntityProperties(properties);

            string query = string.Format("INSERT INTO {0} ({1}) VALUES (@{2}) SELECT CAST(scope_identity() AS int)",
                typeof(T).Name,
                string.Join(", ", entityProperties),
                string.Join(", @", entityProperties));
             
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.ExecuteAsync(query, entity);

            return id;
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            string query = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
            await connection.ExecuteAsync(query,new {Id = id});
        }


        public async Task<List<T>> GetAllAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var result =  await connection.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}");
            return result.ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            string query = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task UpdateAsync(T entity)
        {
            dynamic properties = typeof(T).GetProperties();
            List<string> entityProperties = GetEntityProperties(properties);
            List<string> updateSetProperties = new List<string>();
            entityProperties.ForEach(property =>
            {
                updateSetProperties.Add($"{property} = @{property}");
            });

            string query = string.Format("UPDATE {0} SET  {1} WHERE ID = @Id",
                typeof(T).Name,
                string.Join(", ", updateSetProperties));
                
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.ExecuteAsync(query, entity);
        }

        private List<string> GetEntityProperties(dynamic properties)
        {
            List<string> entityProperties = new List<string>();
            List<string> excludedProperties = new List<string> { "Id","Client","Products","Product"};
            foreach (var property in properties)
            {
                if (!excludedProperties.Contains(property.Name))
                {
                    entityProperties.Add(property.Name);
                }
            }
            return entityProperties;
        }
    }
}

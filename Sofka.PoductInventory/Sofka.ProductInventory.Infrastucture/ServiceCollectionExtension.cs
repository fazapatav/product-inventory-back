using Microsoft.Extensions.DependencyInjection;
using Sofka.ProductInventory.Aplication;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Infrastucture.Repositories;

namespace Sofka.ProductInventory.Infrastucture
{
    public static class ServiceCollectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IProductServices,ProductService>();
        }
    }
}

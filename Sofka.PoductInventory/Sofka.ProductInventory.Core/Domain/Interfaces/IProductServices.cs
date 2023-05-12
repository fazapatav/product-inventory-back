using Sofka.ProductInventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.Core.Domain.Interfaces
{
    public interface IProductServices
    {
        Task CreateProduct(Product product);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.Core.Entities
{
    public class ProductBuy
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

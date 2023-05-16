using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.Core.Dto
{
    public class BuyDto
    {
        public DateTime Date { get; set; }
        public int IdClient { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}

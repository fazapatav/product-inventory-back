using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.Core.Entities
{
    public class Buy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

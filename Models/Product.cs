using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLifeStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int StockQty { get; set; }
        public string Supplier { get; set; }
        public int Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool ActiveStatus { get; set; }
    }
}

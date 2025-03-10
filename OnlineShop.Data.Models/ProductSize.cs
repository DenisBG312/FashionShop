using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Models
{
    public class ProductSize
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int SizeId { get; set; }
        public Size Size { get; set; } = null!;
        public int StockQuantity { get; set; }
    }
}

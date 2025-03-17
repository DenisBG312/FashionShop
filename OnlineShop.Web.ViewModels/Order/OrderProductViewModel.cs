using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.Order
{
    public class OrderProductViewModel
    {
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? SizeId { get; set; }
        public string SizeName { get; set; }
        public string ImgUrl { get; set; }
    }
}

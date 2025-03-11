using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;

namespace OnlineShop.Web.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public int CustomOrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCancelled { get; set; }
        public string SizeName { get; set; }
        public IEnumerable<OrderProductViewModel> OrderProducts { get; set; }
    }
}

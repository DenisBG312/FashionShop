using OnlineShop.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Web.ViewModels.Payment;

namespace OnlineShop.Web.ViewModels.Order
{
    public class OrderDetailsViewAdminModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCancelled { get; set; }
        public string UserName { get; set; }
        public List<ProductDetailsAdminViewModel> Products { get; set; }
        public List<PaymentDetailsAdminViewModel> Payments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Web.ViewModels.Transaction;

namespace OnlineShop.Web.ViewModels.Order
{
    public class OrderIndexViewModel
    {
        public int OrderId { get; set; }
        public int CustomOrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCancelled { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}

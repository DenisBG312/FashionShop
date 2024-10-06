using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.Order
{
    public class OrderTransactionHistoryViewModel
    {
        public int OrderId { get; set; }
        public int CustomOrderNumber { get; set; }
        public List<Data.Models.Payment> Payments { get; set; }
    }
}

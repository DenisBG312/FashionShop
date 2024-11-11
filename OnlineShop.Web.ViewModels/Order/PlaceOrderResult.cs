using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.Order
{
    public class PlaceOrderResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}

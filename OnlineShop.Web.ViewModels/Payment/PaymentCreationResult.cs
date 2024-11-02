using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.Payment
{
    public class PaymentCreationResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}

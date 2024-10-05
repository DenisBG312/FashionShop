using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models.Enums.Payment;

namespace OnlineShop.Web.ViewModels.Payment
{
    public class CreatePaymentViewModel
    {
        public int OrderId { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Models.Enums.Payment
{
    public enum PaymentMethod
    {
        Cash,
        [Display(Name = "Bank Transfer")]
        BankTransfer,
        [Display(Name = "Digital Wallet")]
        DigitalWallet,
        [Display(Name = "Debit Card")]
        DebitCard,
    }
}

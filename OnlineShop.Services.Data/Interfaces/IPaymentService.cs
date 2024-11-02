using OnlineShop.Web.ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentCreationResult> PreparePaymentAsync(int orderId);
        Task<bool> ProcessPaymentAsync(CreatePaymentViewModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Web.ViewModels.Order;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderIndexViewModel>> GetAllOrders(string userId);
        Task<OrderDetailsViewModel?> GetOrderDetails(int orderId);
        Task<bool> ReactivateOrder(int orderId);
        Task<bool> CancelOrder(int orderId);
        Task<bool> FinalizeOrder(int orderId);
    }
}

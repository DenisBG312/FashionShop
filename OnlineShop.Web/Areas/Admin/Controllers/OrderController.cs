using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Data.Interfaces;
using System.Security.Claims;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Order;
using X.PagedList.Extensions;
using static OnlineShop.Common.EntityValidationConstants;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(int? page, int pageSize = 5)
        {
            int pageNumber = page ?? 1;
            var orders = await _orderService.GetAllOrders();
            var pagedOrders = orders.ToPagedList(pageNumber, pageSize);

            return View(pagedOrders);
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id, int currentPage)
        {
            var result = await _orderService.CancelOrder(id);
            if (result)
            {
                TempData["Success"] = "Order canceled successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to cancel the order.";
            }

            return RedirectToAction("Index", "Order", new {page = currentPage});
        }

        [HttpPost]
        public async Task<IActionResult> Reactivate(int id, int currentPage)
        {
            var result = await _orderService.ReactivateOrder(id);
            if (result)
            {
                TempData["Success"] = "Order reactivated successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to reactivate the order.";
            }

            return RedirectToAction("Index", "Order", new {page = currentPage});
        }

        public async Task<IActionResult> Details(int id)
        {
            var orderDetails = await _orderService.GetOrderAdminDetails(id);

            if (orderDetails == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction("Index");
            }

            return View(orderDetails);
        }
    }
}

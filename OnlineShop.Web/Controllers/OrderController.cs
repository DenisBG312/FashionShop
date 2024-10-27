using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Order;
using System.Security.Claims;
using OnlineShop.Web.ViewModels.Transaction;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text.pdf.draw;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Services.Data.Interfaces;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IOrderService _orderService;

        public OrderController(ApplicationDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllOrders(userId);

            return View(orders.ToList());
        }


        public async Task<IActionResult> Details(int id)
        {
            var orderDetails = await _orderService.GetOrderDetails(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Reactivate(int id)
        {
            var success = await _orderService.ReactivateOrder(id);

            if (!success)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Order reactivated successfully.";
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var success = await _orderService.CancelOrder(id);

            if (!success)
            {
                return NotFound();
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeOrder(int id)
        {
            var success = await _orderService.FinalizeOrder(id);

            if (!success)
            {
                TempData["ErrorMessage"] = "Insufficient stock for one or more products in the order.";
                return RedirectToAction("Details", new { id });
            }

            TempData["SuccessMessage"] = "Thank you for your order! Your order has been finalized. \u2705";

            return RedirectToAction("Details", new { id });
        }


        public async Task<IActionResult> TransactionHistory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await _orderService.GetTransactionHistoryAsync(id, userId);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        public async Task<IActionResult> ExportToPdf(int orderId)
        {
            var pdfBytes = await _orderService.GenerateOrderTransactionPdfAsync(orderId);

            if (pdfBytes == null)
            {
                return NotFound();
            }

            var fileName = $"TransactionHistory_{orderId}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}

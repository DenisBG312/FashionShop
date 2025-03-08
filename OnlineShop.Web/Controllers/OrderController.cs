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
using Stripe.Checkout;
using System.Linq;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;

        public OrderController(IOrderService orderService, IConfiguration configuration)
        {
            _orderService = orderService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession(int orderId)
        {
            var order = await _orderService.GetOrderDetails(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var request = HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = order.OrderProducts.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(item.UnitPrice * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName,
                            Images = new List<string> {item.ImgUrl}
                        },
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                SuccessUrl = $"{domain}/Order/CheckoutSuccess?orderId={orderId}&session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/Order/CheckoutCancel?orderId={orderId}",
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Json(new { sessionId = session.Id });
        }

        public async Task<IActionResult> CheckoutSuccess(int orderId)
        {
            try
            {
                var success = await _orderService.FinalizeOrder(orderId);
                if (!success)
                {
                    TempData["ErrorMessage"] = "Payment was processed but there was an issue finalizing the order.";
                }
                else
                {
                    TempData["SuccessMessage"] = "Payment successful and order finalized!";
                }
                return RedirectToAction("Details", new { id = orderId });
            }
            catch (Exception ex)
            {
                return Content($"An error occurred during CheckoutSuccess: {ex.Message}");
            }
        }

        public IActionResult CheckoutCancel(int orderId)
        {
            TempData["ErrorMessage"] = "Payment cancelled. Please try again.";
            return RedirectToAction("Details", new { id = orderId });
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllOrdersForUser(userId);

            return View(orders.ToList());
        }


        public async Task<IActionResult> Details(int id)
        {
            var orderDetails = await _orderService.GetOrderDetails(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            ViewBag.StripePublishableKey = _configuration["Stripe:PublishableKey"];
            return View(orderDetails);
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

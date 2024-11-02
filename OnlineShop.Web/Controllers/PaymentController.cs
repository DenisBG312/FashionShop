using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Payment;

namespace OnlineShop.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymentService _paymentService;

        public PaymentController(ApplicationDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int orderId)
        {
            var result = await _paymentService.PreparePaymentAsync(orderId);

            if (!result.Success)
            {
                TempData["PaymentError"] = result.ErrorMessage;
                return RedirectToAction("Details", "Order", new { id = orderId });
            }

            var viewModel = new CreatePaymentViewModel
            {
                OrderId = orderId,
                TotalAmount = result.RemainingAmount
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var preparationResult = await _paymentService.PreparePaymentAsync(model.OrderId);
                model.TotalAmount = preparationResult.RemainingAmount;
                return View(model);
            }

            var isProcessed = await _paymentService.ProcessPaymentAsync(model);

            if (!isProcessed)
            {
                TempData["PaymentError"] = "Failed to process the payment. Please try again.";
                return RedirectToAction("Details", "Order", new { id = model.OrderId });
            }

            TempData["PaymentSuccess"] = true;
            return RedirectToAction("Details", "Order", new { id = model.OrderId });
        }

        public async Task<IActionResult> ConfirmPayment(int orderId, decimal amount)
        {
            var result = await _paymentService.ConfirmPaymentAsync(orderId, amount);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Create", new CreatePaymentViewModel
                {
                    OrderId = orderId,
                    TotalAmount = amount
                });
            }

            TempData["PaymentSuccess"] = true;
            return RedirectToAction("Details", "Order", new { id = orderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int paymentId)
        {
            var success = await _paymentService.CancelPaymentAsync(paymentId);

            if (!success)
            {
                return NotFound();
            }

            TempData["PaymentCancelled"] = true;

            return RedirectToAction("Details", "Order", new { id = paymentId });
        }
    }
}

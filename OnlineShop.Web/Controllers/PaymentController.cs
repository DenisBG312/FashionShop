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
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var remainingAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity) - order.Payments.Sum(p => p.Amount);

            if (amount < remainingAmount)
            {
                ModelState.AddModelError(string.Empty, "Payment amount is less than the total amount due.");
                return View("Create", new CreatePaymentViewModel
                {
                    OrderId = orderId,
                    TotalAmount = remainingAmount
                });
            }

            // Create a new payment entry with status set to Pending
            var payment = new Payment
            {
                OrderId = orderId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                Status = Status.Pending // Set status to Pending by default
            };

            _context.Payments.Add(payment);

            // Reduce stock for each product in the order
            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= orderProduct.Quantity; // Decrease stock
                }
            }

            await _context.SaveChangesAsync();

            TempData["PaymentSuccess"] = true;

            return RedirectToAction("Details", "Order", new { id = orderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int paymentId)
        {
            var payment = await _context.Payments.Include(p => p.Order).ThenInclude(o => o.OrderProducts).FirstOrDefaultAsync(p => p.Id == paymentId);

            if (payment == null)
            {
                return NotFound();
            }

            // Update the payment status to Cancelled
            payment.Status = Status.Cancelled;

            // Refund the stock for each product in the order
            foreach (var orderProduct in payment.Order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product != null)
                {
                    product.StockQuantity += orderProduct.Quantity; // Restore stock
                }
            }

            await _context.SaveChangesAsync();

            // Set a flag to show the cancellation message in the view
            TempData["PaymentCancelled"] = true;

            return RedirectToAction("Details", "Order", new { id = payment.OrderId });
        }
    }
}

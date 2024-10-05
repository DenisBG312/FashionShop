using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Web.ViewModels.Payment;

namespace OnlineShop.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new CreatePaymentViewModel
            {
                OrderId = orderId,
                TotalAmount = order.TotalAmount - order.Payments.Sum(p => p.Amount) // Calculate remaining amount
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    OrderId = model.OrderId,
                    Amount = model.Amount,
                    PaymentMethod = model.PaymentMethod,
                    PaymentDate = DateTime.Now,
                    Status = Status.Completed // Set status as completed for now
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Order", new { id = model.OrderId });
            }

            return View(model);
        }

    }
}

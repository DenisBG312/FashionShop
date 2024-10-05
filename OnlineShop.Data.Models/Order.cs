using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public decimal TotalAmount { get; set; }

        public void CalculateTotalAmount(IEnumerable<OrderProduct> orderProduct)
        {
            TotalAmount = orderProduct.Sum(item => item.TotalPrice);
        }

        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
        public bool IsCompleted => Payments.Sum(p => p.Amount) >= TotalAmount;
    }
}

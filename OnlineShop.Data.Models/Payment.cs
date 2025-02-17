using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShop.Common;
using OnlineShop.Data.Models.Enums.Payment;

namespace OnlineShop.Data.Models
{
    using static EntityValidationConstants.Payment;
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(AmountMinValue, AmountMaxValue)]

        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        [Required]
        public Status Status { get; set; } = Status.Pending;

        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Common;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;

namespace OnlineShop.Data.Configuration
{
    using static EntityValidationConstants.Payment;
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasData(GeneratePayments());
        }

        private IEnumerable<Payment> GeneratePayments()
        {
            IEnumerable<Payment> payments = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    Amount = 144.98m,
                    PaymentDate = new DateTime(2024, 10, 2),
                    Status = Status.Pending,
                    OrderId = 1
                },
            };


            return payments;
        }
    }
}

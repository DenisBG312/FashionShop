using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Common;

namespace OnlineShop.Data.Models
{
    using static EntityValidationConstants.Gender;
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }

}

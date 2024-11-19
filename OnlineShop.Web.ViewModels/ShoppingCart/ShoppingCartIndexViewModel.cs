using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartIndexViewModel
    {
        public int ShoppingCartId { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<ShoppingCartProductViewModel> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.Wishlist
{
    public class GetAllWishlistProductsViewModel
    {
        public int Id { get; set; }
        public string ProductImgUrl { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool IsOnSale { get; set; }
        public int? DiscountPercentage { get; set; }

    }
}

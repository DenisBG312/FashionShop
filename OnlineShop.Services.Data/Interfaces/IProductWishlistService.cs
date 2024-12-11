using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Wishlist;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IProductWishlistService
    {
        Task<bool> AddToWishlistAsync(string userId, int productId);
        Task<bool> RemoveFromWishlistAsync(string userId, int productId);
        Task<IEnumerable<GetAllWishlistProductsViewModel>> GetUserWishlistAsync(string userId);
    }
}

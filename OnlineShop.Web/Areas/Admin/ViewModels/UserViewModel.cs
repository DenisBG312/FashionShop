using OnlineShop.Data.Models;

namespace OnlineShop.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}

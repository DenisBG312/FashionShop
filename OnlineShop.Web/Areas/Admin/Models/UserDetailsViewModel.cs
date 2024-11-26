using OnlineShop.Data.Models;

namespace OnlineShop.Web.Areas.Admin.Models
{
    public class UserDetailsViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImgUrl { get; set; }
    }
}

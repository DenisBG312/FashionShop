namespace OnlineShop.Web.Areas.Admin.Models
{
    public class AssignRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}

using System.Diagnostics;
using System.Drawing.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Data;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int? statusCode = null)

        {
            if (!statusCode.HasValue)
            {
                return this.View();
            }

            if (statusCode == 404)
            {
                return this.View("Error404");
            }
            else if (statusCode == 401 || statusCode == 403)
            {
                return this.View("Error403");
            }

            return this.View("Error500");
        }
    }
}

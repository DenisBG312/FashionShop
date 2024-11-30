using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(int? statusCode)
        {
            if (!statusCode.HasValue)
            {
                return View();
            }

            if (statusCode == 404)
            {
                return View("Error404");
            }
            else if (statusCode == 403 || statusCode == 401)
            {
                return View("Error403");
            }

            return View("Error500");
        }

        public IActionResult ChangeLanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                lang = "en";
            }

            Response.Cookies.Append("Language", lang);
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }
    }
}

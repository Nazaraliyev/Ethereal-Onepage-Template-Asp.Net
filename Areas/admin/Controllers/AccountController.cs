using Microsoft.AspNetCore.Mvc;

namespace Ethereal_Onepage_Template_Asp.Net.Areas.admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SessionAndCookies.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return View();   // empty box: show the form again

            HttpContext.Session.SetString("User", name);     // remember the name on the server
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();                     // forget this user
            return RedirectToAction("Index", "Home");
        }
    }
}

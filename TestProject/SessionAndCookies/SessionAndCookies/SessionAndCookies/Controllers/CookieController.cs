using Microsoft.AspNetCore.Mvc;

namespace SessionAndCookies.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult Index()
        {
            // Read the cookie the browser sent; "light" if it is missing
            ViewBag.Theme = Request.Cookies["theme"] ?? "light";
            return View();
        }

        public IActionResult SetTheme()
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),  // lives 7 days
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };
            Response.Cookies.Append("theme", "dark", options);
            return RedirectToAction("Index");
        }

        public IActionResult ClearTheme()
        {
            Response.Cookies.Delete("theme");
            return RedirectToAction("Index");
        }
    }
}

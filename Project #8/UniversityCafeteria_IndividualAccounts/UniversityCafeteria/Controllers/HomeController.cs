using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Error() => View();
}

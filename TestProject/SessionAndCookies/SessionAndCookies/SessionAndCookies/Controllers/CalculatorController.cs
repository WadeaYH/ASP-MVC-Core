using Microsoft.AspNetCore.Mvc;
using SessionAndCookies.Models;

namespace SessionAndCookies.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CalculatorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CalculatorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Calculate(model);

            if (model.Result != null)
            {
                HttpContext.Session.SetString("Last", model.Result.ToString()!);   // bonus: remember in session
            }
            return View(model);
        }

        private static void Calculate(CalculatorViewModel m)
        {
            decimal a = m.Number1;        // decimal so 7 / 2 = 3.5
            decimal b = m.Number2;

            switch (m.Operation)
            {
                case "+": m.Result = a + b; break;
                case "-": m.Result = a - b; break;
                case "*": m.Result = a * b; break;
                case "/":
                    if (b == 0) m.Error = "Cannot divide by zero";
                    else m.Result = a / b;
                    break;
                default: m.Error = "Choose an operation"; break;
            }
        }
    }
}

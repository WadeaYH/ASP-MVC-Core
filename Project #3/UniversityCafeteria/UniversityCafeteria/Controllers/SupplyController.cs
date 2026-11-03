using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace UniversityCafeteria.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Categories = new List<string> { "Drinks", "Food", "Snacks", "Hot Drinks", "Cold Drinks" };
            return View();
        }

        public IActionResult Products()
        {
            ViewData["PageTitle"] = "Cafeteria Products";
            ViewData["ManagerName"] = "Ahmad Ali";
            ViewData["NumberOfProducts"] = 5;
            ViewData["Products"] = new List<string> { "Water", "Orange Juice", "Coffee", "Sandwich", "Chocolate" };
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Suppliers()
        {
            ViewBag.PageTitle = "Our Suppliers";
            ViewBag.NumberOfSuppliers = 3;
            ViewBag.Food = new List<string> { "Fresh Food Company", "Happy Drinks Company", "Daily Supplies Company" };
            return View();
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRequest()
        {
            string employeeName = Request.Form["EmployeeName"];
            string productName = Request.Form["ProductName"];
            string quantity = Request.Form["Quantity"];
            string requiredDate = Request.Form["RequiredDate"];

            if (string.IsNullOrWhiteSpace(employeeName))
            {
                ViewBag.Error = "Employee name is required.";
                return View();
            }

            TempData["Message"] = $"Request for {quantity} x {productName} by {employeeName} (needed by {requiredDate}) was submitted.";
            return RedirectToAction("About");
        }

        [HttpGet]
        public IActionResult CheckQuantity(int quantity)
        {
            if (quantity < 20) return Content("Small Order");
            else if (quantity >= 20 && quantity <= 50) return Content("Medium Order");
            else if (quantity > 50) return Content("Large Order");
            return Content("GG BRO");
        }

    }
}

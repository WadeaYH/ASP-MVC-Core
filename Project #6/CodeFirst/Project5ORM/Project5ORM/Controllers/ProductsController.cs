using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.NativeInterop;
using Project5ORM.Models.CodeFirst;

namespace Project5ORM.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CafeteriaEmployeesDBContext _context;

        public ProductsController(CafeteriaEmployeesDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Products = _context.Products.ToList(); //Read => SELECT * FROM Suppliers;
            return View(Products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _context.Products.Add(product); //Create
            try
            {
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }

        }
    }
}

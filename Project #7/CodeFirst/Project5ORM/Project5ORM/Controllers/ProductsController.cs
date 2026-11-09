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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var products = _context.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(products);
            }

            _context.Products.Update(products);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var products = _context.Products.Find(id); //Read one => SELECT * FROM Products WHERE Id = id;
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var products = _context.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products); //Delete
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }
    }
}

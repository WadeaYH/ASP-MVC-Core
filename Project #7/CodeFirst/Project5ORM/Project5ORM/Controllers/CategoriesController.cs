using Microsoft.AspNetCore.Mvc;
using Project5ORM.Models.CodeFirst;

namespace Project5ORM.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CafeteriaEmployeesDBContext _context;

        public CategoriesController(CafeteriaEmployeesDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList(); //Read => SELECT * FROM Categories;

            var products = _context.Products.ToList(); //Read => SELECT * FROM Products;
            ViewBag.Products = products;
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Add(category); //Create
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Update(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var category = _context.Categories.Find(id); //Read one => SELECT * FROM Categories WHERE Id = id;
            if (category == null)
            {
                return NotFound();
            }

            var products = _context.Products.ToList(); //Read => SELECT * FROM Products;
            ViewBag.Products = products;

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category); //Delete
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }
    }
}

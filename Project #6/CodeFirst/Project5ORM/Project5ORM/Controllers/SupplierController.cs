using Microsoft.AspNetCore.Mvc;
using Project5ORM.Models.CodeFirst;

namespace Project5ORM.Controllers
{
    public class SupplierController : Controller
    {
        private readonly CafeteriaEmployeesDBContext _context;

        public SupplierController(CafeteriaEmployeesDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Suppliers = _context.Suppliers.ToList(); //Read => SELECT * FROM Suppliers;
            return View(Suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }

            _context.Suppliers.Add(supplier); //Create
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }
    }
}

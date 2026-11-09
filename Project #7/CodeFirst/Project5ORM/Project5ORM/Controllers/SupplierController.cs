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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var suppliers = _context.Suppliers.Find(id);
            if (suppliers == null)
            {
                return NotFound();
            }
            return View(suppliers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Supplier suppliers)
        {
            if (id != suppliers.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(suppliers);
            }

            _context.Suppliers.Update(suppliers);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var suppliers = _context.Suppliers.Find(id); //Read one => SELECT * FROM Suppliers WHERE Id = id;
            if (suppliers == null)
            {
                return NotFound();
            }

            return View(suppliers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var suppliers = _context.Suppliers.Find(id);
            if (suppliers == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(suppliers); //Delete
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }
    }
}

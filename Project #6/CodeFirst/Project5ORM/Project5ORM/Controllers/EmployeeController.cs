using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5ORM.Models.CodeFirst;

namespace Project5ORM.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CafeteriaEmployeesDBContext _context;
        public EmployeeController(CafeteriaEmployeesDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList(); //Read => SELECT * FROM Employees;
            return View(employees);
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

            _context.Employees.Add(category); //Create
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }
    }
}

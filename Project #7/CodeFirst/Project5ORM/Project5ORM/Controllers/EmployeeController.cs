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
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            _context.Employees.Add(employee); //Create
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            _context.Employees.Update(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var employee = _context.Employees.Find(id); //Read one => SELECT * FROM Employees WHERE Id = id;
            if (employee == null)
            {
                return NotFound();
            }

            var orders = _context.Orders.Where(o => o.EmployeeId == id).ToList(); //Read all orders for the employee
            ViewBag.Orders = orders;
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee); //Delete
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }
    }
}

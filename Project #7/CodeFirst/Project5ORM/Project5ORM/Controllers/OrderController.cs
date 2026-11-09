using Microsoft.AspNetCore.Mvc;
using Project5ORM.Models.CodeFirst;

namespace Project5ORM.Controllers
{
    public class OrderController : Controller
    {
        private readonly CafeteriaEmployeesDBContext _context;

        public OrderController(CafeteriaEmployeesDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList(); //Read => SELECT * FROM Orders;
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (!_context.Employees.Any(e => e.Id == order.EmployeeId))
            {
                ViewBag.Error = "This employee doesn't exist.";
                return View(order);
            }

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            _context.Orders.Add(order); //Create
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order)
        {

            if (!_context.Employees.Any(e => e.Id == order.EmployeeId))
            {
                ViewBag.Error = "This employee doesn't exist.";
                return View(order);
            }

            if (id != order.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            _context.Orders.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders.Find(id); //Read one => SELECT * FROM Orders WHERE Id = id;
            if (order == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find((order.EmployeeId));
            ViewBag.employee = employee;
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order); //Delete
            _context.SaveChanges(); //Save

            return RedirectToAction("Index");
        }
    }
}

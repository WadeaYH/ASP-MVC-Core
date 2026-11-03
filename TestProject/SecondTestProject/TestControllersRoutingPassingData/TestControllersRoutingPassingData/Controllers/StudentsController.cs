using Microsoft.AspNetCore.Mvc;

namespace TestControllersRoutingPassingData.Controllers
{
    public class StudentsController : Controller
    {
        private static List<string> students = new List<string> { "Sara", "Omar", "Lina" };

        // /Students  or  /Students/Index
        public IActionResult Index()
        {
            ViewData["Title"] = "Students";              // ViewData: string key
            ViewBag.Count = students.Count;              // ViewBag: dot syntax
            ViewBag.Names = students;
            return View();
        }

        public IActionResult Hello()
        {
            return Content("Hello Sara");
        }

        public IActionResult Details(int id)
        {
            return Content("Student number " + id);
        }

        // GET /Students/Create -> show the empty form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST /Students/Create -> form submitted
        [HttpPost]
        public IActionResult Create(string name)
        {
            students.Add(name);                          // "save" the student
            TempData["Msg"] = name + " saved";           // survives the redirect
            return RedirectToAction("Index");
        }
    }
}

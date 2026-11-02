// Controllers/StudentController.cs
using FirstMVCCore.Models;
using FirstMVCCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCCore.Controllers;
public class StudentController : Controller          // like @Controller (not @RestController)
{
    private readonly IStudentService _service;

    public StudentController(IStudentService service)   // constructor injection (same as Spring)
    {
        _service = service;
    }

    // ========== GET (read list) ==========
    // URL: GET /Student   (or /Student/Index)
    public IActionResult Index()
    {
        var students = _service.GetAll();
        return View(students);       // opens Views/Student/Index.cshtml and passes the list
    }

    // ========== GET (show empty form) ==========
    // URL: GET /Student/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();               // just shows the empty form
    }

    // ========== POST (submit the form) ==========
    // URL: POST /Student/Create
    [HttpPost]
    public IActionResult Create(Student student)   // form fields auto-fill the object
    {
        _service.Add(student);
        return RedirectToAction("Index");          // like "redirect:/students"
    }

    // ========== GET (show form filled with existing data) ==========
    // URL: GET /Student/Edit/1
    [HttpGet]
    public IActionResult Edit(int id)              // id comes from the URL (like @PathVariable)
    {
        var student = _service.GetById(id);
        if (student == null) return NotFound();    // returns 404
        return View(student);
    }

    // ========== POST (save changes) ==========
    // URL: POST /Student/Edit
    [HttpPost]
    public IActionResult Edit(Student student)
    {
        _service.Update(student);
        return RedirectToAction("Index");
    }

    // ========== POST (delete) ==========
    // URL: POST /Student/Delete/1
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction("Index");
    }
}
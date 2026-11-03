using Microsoft.AspNetCore.Mvc;

namespace TestControllersRoutingPassingData.Controllers
{
    [Route("courses")]
    //[Route("wadea")]
    //[Route("test")]

    public class CoursesController : Controller
    {
        [HttpGet("")]
        public IActionResult List()
        {
            return Content("All courses");
        }

        [HttpGet("{id:int}")]
        public IActionResult Show(int id)
        {
            return Content("Course " + id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutForm() => View();
    }
}

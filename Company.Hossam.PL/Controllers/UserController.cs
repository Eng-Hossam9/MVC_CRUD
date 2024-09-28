using Microsoft.AspNetCore.Mvc;

namespace Company.Hossam.PL.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

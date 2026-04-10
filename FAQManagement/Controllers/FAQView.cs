using Microsoft.AspNetCore.Mvc;

namespace FAQManagement.Controllers
{
    public class FAQ : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
    }
}

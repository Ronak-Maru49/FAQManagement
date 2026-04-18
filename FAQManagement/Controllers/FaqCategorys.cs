using Microsoft.AspNetCore.Mvc;

namespace FAQManagement.Controllers
{
    public class FaqCategorys : Controller
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

using Microsoft.AspNetCore.Mvc;

namespace FAQManagement.Controllers
{
    public class AiFaqGen : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

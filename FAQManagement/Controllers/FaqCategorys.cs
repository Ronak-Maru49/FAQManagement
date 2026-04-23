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

        public IActionResult AddSubCategory()
        {
            return View();
        }

        public IActionResult SubCategoryList()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
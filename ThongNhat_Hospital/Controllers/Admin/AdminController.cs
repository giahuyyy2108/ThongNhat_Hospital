using Microsoft.AspNetCore.Mvc;

namespace ThongNhat_Hospital.Controllers.Admin
{
    public class AdminController : Controller
    {



        public IActionResult Index()
        {
            return View();
        }
    }
}

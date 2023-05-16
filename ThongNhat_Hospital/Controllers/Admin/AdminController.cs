using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThongNhat_Hospital.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Role()
        {
            return View();
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThongNhat_Hospital.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Authorize]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

       

    }
}

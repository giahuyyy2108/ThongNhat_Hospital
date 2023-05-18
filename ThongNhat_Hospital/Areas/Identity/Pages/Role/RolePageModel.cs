using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Areas.Identity.Pages.Role
{
    [Authorize]
    public class RolePageModel : PageModel
    {
        protected RoleManager<IdentityRole> _rolemanager;
        protected DataBaseContext _context;
        public RolePageModel(RoleManager<IdentityRole> rolemanager, DataBaseContext context) 
        {
            _rolemanager= rolemanager;
            _context= context;
        }
    }
}

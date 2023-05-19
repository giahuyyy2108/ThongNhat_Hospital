using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Areas.Identity.Pages.Role
{
    //[Authorize(Roles = "admin")]
    public class IndexModel : UserPageModel
    {
        public IndexModel(RoleManager<IdentityRole> rolemanager, DataBaseContext context) : base(rolemanager, context)
        {
        }
        public List<IdentityRole> role { get; set; }
        public async Task OnGet()
        {
            role = await _rolemanager.Roles.OrderBy(r => r.Name).ToListAsync();
        }

        public void OnPost() => RedirectToPage();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Areas.Identity.Pages.User1
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {

        private readonly UserManager<User> _usermanager;
        private readonly DataBaseContext _context;

        public IndexModel(UserManager<User> userManager, DataBaseContext context)
        {
            _usermanager = userManager;
            _context = context;
        }

        public class UserAndRole : User
        {
            public string Rolenames { get; set; }
        }

        public List<UserAndRole> users { get; set; }
        public async Task OnGet()
        {
            var qr =  _usermanager.Users.OrderBy(r => r.UserName);

            var qr1 = qr.Select(u => new UserAndRole()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                hoten = u.hoten
            });

            users = await qr1.ToListAsync();

            foreach(var user in users)
            {
                var roles = await _usermanager.GetRolesAsync(user);
                user.Rolenames = string.Join(", ", roles.ToList());
            }
        }

        public void OnPost() => RedirectToPage();
    }
}

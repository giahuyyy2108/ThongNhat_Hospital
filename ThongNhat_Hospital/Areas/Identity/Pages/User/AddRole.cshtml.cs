using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Areas.Identity.Pages.Account.Manage1
{
    //[Authorize(Roles = "Admin")]
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

      

        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty]
        public string[] Rolename { get; set; }
       
        public User user { get; set; }

        public SelectList allRoles { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có người dùng");
            }

            user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Không thấy user có ID: {id}");
            }

            Rolename= ( await _userManager.GetRolesAsync(user)).ToArray<string>();


            List<string> rolenames = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
            allRoles = new SelectList(rolenames);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có người dùng");
            }

            user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Không thấy user có ID: {id}");
            }

            var oldRole = (await _userManager.GetRolesAsync(user)).ToArray();

            var deleteRole = oldRole.Where(r => !Rolename.Contains(r));

            var addRole = Rolename.Where(r=> !oldRole.Contains(r));

            List<string> rolenames = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
            allRoles = new SelectList(rolenames);

            var resultDelete= await _userManager.RemoveFromRolesAsync(user,deleteRole);
            if (!resultDelete.Succeeded)
            {
                resultDelete.Errors.ToList().ForEach(erro =>
                {
                    ModelState.AddModelError(string.Empty, erro.Description);
                });
                return Page();
            }

            var resultAdd = await _userManager.AddToRolesAsync(user, addRole);
            if (!resultAdd.Succeeded)
            {
                resultAdd.Errors.ToList().ForEach(erro =>
                {
                    ModelState.AddModelError(string.Empty, erro.Description);
                });
                return Page();
            }


            return RedirectToPage("./Index");
        }
    }
}

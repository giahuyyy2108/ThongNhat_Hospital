using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Areas.Identity.Pages.Role
{
    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> rolemanager, DataBaseContext context) : base(rolemanager, context)
        {
        }

        public class InputModel
        {
            [Display(Name= "Tên Role")]
            [Required(ErrorMessage = "Phải nhập tên {0}")]
            [StringLength(50,MinimumLength =4,ErrorMessage ="{0} Phải dài {2} đến {1} ký tự")]
            public string Name { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IdentityRole role { get; set; }

        public async Task<IActionResult> OnGet(string roleid)
        {
            if(roleid == null )
                return NotFound("Không tìm tháy được Role cần sửa");

            role = await _rolemanager.FindByIdAsync(roleid);
            if(role != null)
            {
                Input = new InputModel
                {
                    Name = role.Name
                };
                return Page();
            }

            return NotFound("Không tìm tháy được Role cần sửa");

        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if(roleid == null)
                return NotFound("Không tìm tháy được Role cần sửa");

            role = await _rolemanager.FindByIdAsync(roleid);
            if (role == null)
                return NotFound("Không tìm tháy được Role cần sửa");


            if (!ModelState.IsValid)
            {
                return Page();
            }
            role.Name= Input.Name;
            var result= await _rolemanager.UpdateAsync(role);

            if(result.Succeeded)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(erro =>
                {
                    ModelState.AddModelError(string.Empty, erro.Description);
                });
            }

            return Page();
        }
    }
}

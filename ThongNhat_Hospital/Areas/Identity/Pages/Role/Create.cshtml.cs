using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Areas.Identity.Pages.Role
{
    public class CreateModel : UserPageModel
    {
        public CreateModel(RoleManager<IdentityRole> rolemanager, DataBaseContext context) : base(rolemanager, context)
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

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var NewRole = new IdentityRole(Input.Name);
            var result= await _rolemanager.CreateAsync(NewRole);
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

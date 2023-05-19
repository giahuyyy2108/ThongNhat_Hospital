using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThongNhat_Hospital.Models;
using static ThongNhat_Hospital.Areas.Identity.Pages.User1.IndexModel;

namespace ThongNhat_Hospital.Areas.Identity.Pages.User1
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CreateModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public class InputModel
        {
            [Display(Name= "Nhập họ tên")]
            [Required(ErrorMessage = "Phải nhập tên {0}")]
            public string Hoten { get; set; }

            [Display(Name = "Nhập tên đăng nhập")]
            [Required(ErrorMessage = "Phải nhập tên {0}")]
            public string UserName { get; set; }

            [Display(Name = "Nhập Email")]
            [Required(ErrorMessage = "Phải nhập tên {0}")]
            public string Email { get; set; }

        }
        public User user { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public static string GetRandomPassword(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Random random = new Random();

            if (!ModelState.IsValid)
            {
                return Page();
            }



            var user = new User { UserName = Input.UserName, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, GetRandomPassword(9));


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

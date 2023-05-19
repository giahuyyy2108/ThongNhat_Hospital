using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ThongNhat_Hospital.Models;
using static ThongNhat_Hospital.Areas.Identity.Pages.User1.IndexModel;

namespace ThongNhat_Hospital.Areas.Identity.Pages.User1
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;


        public CreateModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
             IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public class InputModel
        {
            [Display(Name= "Nhập họ tên")]
            [Required(ErrorMessage = "Phải nhập tên {0}")]
            public string Hoten { get; set; }

            //[Display(Name = "Nhập tên đăng nhập")]
            //[Required(ErrorMessage = "Phải nhập tên {0}")]
            //public string UserName { get; set; }

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

            String[] parts = Input.Email.Split(new[] { '@' });

            Random random = new Random();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            string mk = GetRandomPassword(9);
            var user = new User { UserName = parts[0], Email = Input.Email };
            var result = await _userManager.CreateAsync(user,mk);
            if(result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: "Xác nhận tài khoản",
                    values: new { area = "Identity", userId = user.Id, code = code},
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Hãy xác nhận mail sau để đăng nhập tài khoản <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Xác nhận</a>.<br>" +
                    $"Tài khoản: {user.UserName}<br>" +
                    $"Mật khẩu: {mk}");

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

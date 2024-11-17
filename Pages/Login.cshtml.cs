using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Bcpg;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.DTOs.Outputs;
using System.Text.Json;


namespace WebApp.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private LoginService _loginService = new LoginService();
        internal UserOutput user { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                user = await _loginService.Logar(email, password);
                if (user == null) throw new ArgumentNullException(nameof(user));

                TempData["email"] = JsonSerializer.Serialize(email);
                TempData["User"] = JsonSerializer.Serialize(user);

                //TempData["javascript"] = $@"
                //    sessionStorage.setItem('email', '{email}');
                //    sessionStorage.setItem('user', '{JsonSerializer.Serialize(user)}');
                //";
                return RedirectToPage("/PcPage");
            }
            catch (Exception ex) { Console.WriteLine(ex); return Page(); }
        }
    }
}

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
        internal UserOutput User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                User = await _loginService.Logar(Email, Password);
                if (User == null) throw new ArgumentNullException(nameof(User));

                if (User.Name == "Cersam Barreiro")
                {
                    TempData["Email"] = JsonSerializer.Serialize(Email);
                    return RedirectToPage("/CersamPage");
                }
                TempData["Email"] = JsonSerializer.Serialize(Email);
                return RedirectToPage("/PcPage");
            }
            catch (Exception ex) { Console.WriteLine(ex); return Page(); }
        }
    }
}

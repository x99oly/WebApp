using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

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

                return RedirectToPage("/PcPage");
            }
            catch (Exception ex) { Console.WriteLine(ex); return Page(); }
        }
    }
}

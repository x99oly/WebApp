using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.DTOs.Outputs;
using System.Text.Json;
using WebApp.Domain.DomainSrv;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages
{
    public class PcPageModel : PageModel
    {
        private GetPcOutputSrv _pcSrv = new GetPcOutputSrv();
        private string email;
        public UserOutput User { get; set; }
        public PcOutput Pc { get; set; }

        public async void OnGet()
        {
            try
            {
                if (TempData != null)
                {
                    email = JsonSerializer.Deserialize<string>(TempData["email"].ToString());
                    User = JsonSerializer.Deserialize<UserOutput>(TempData["User"].ToString());
                }
                if (User == null || email == null) await Redirect();

                Pc = await _pcSrv.Srv(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await Redirect();
            }
        }

        private async Task<IActionResult> Redirect()
        {
            return RedirectToPage("/Login");
        }
    }
}

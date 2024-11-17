using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.DTOs.Outputs;
using System.Text.Json;
using WebApp.Domain.DomainSrv;
using System.ComponentModel.DataAnnotations;
using WebApp.Domain.Entities;

namespace WebApp.Pages
{
    public class PcPageModel : PageModel
    {
        private GetPcOutputSrv _pcSrv = new GetPcOutputSrv();
        UpdateDonationSrv _dnSrv = new UpdateDonationSrv();
        private string email;

        [BindProperty]
        public string cod { get; set; }
        public UserOutput user { get; set; }
        public PcOutput Pc { get; set; }

        public async Task OnGet()
        {
            try
            {
                if (TempData != null)
                {
                    email = JsonSerializer.Deserialize<string>(TempData["email"].ToString());
                    user = JsonSerializer.Deserialize<UserOutput>(TempData["User"].ToString());
                }
                if (user == null || email == null) await Redirect();

                Pc = await _pcSrv.Srv(email);
                if (Pc == null) await Redirect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await Redirect();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            Pc pc = new Pc();
            string pcCod = await pc.GetCod("oliveira.samuel.edu@gmail.com");

            if (string.IsNullOrEmpty(pcCod))
            {
                ViewData["Success"] = false;
                return RedirectToPage();
            }

            await _dnSrv.Srv(cod, pcCod, null, null);
            ViewData["Success"] = true;
            return RedirectToPage();
        }

        private async Task<IActionResult> Redirect()
        {
            return RedirectToPage("/Login");
        }
    }
}

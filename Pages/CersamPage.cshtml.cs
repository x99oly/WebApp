using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Pages
{
    public class CersamPageModel : PageModel
    {
        private MySqlPersistence _data = new MySqlPersistence();
        private readonly GetPcOutputSrv _pcSrv = new GetPcOutputSrv();  // Adicionei readonly
        private readonly UpdateDonationSrv _donationSrv = new UpdateDonationSrv(); // Adicionei readonly
        private readonly DonationsSrv _donationsSrv = new DonationsSrv();

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? DonationCod { get; set; }
        public async Task OnGet()
        {
            if (TempData["Email"] is string email)
            {
                if (string.IsNullOrEmpty(email)) await RedirectToLogin();
                Email = email.Trim('"').Replace("\\", "");
            }
        }

        public async void OnPost()
        {
            Cersam c = await _data.GetCersamAsync();
            if (c == null) await RedirectToLogin();

            await _donationSrv.Srv(DonationCod, null, c.cod, null, "donation");

            await RedirectToThisPage();
        }

        private async Task<IActionResult> RedirectToThisPage()
        {
            TempData["Email"] = JsonSerializer.Serialize(Email);
            return RedirectToPage();
        }
        private async Task<IActionResult> RedirectToLogin()
        {
            return RedirectToPage("/Login");
        }
    }
}

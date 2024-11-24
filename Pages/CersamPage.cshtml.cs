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
        public string? Cod_Lot { get; set; }
        public async Task OnGet()
        {

        }

        public async void OnPost()
        {
            Cersam c = await _data.GetCersamAsync();
            if (c == null) await RedirectToLogin();

            if (string.IsNullOrEmpty(Cod_Lot)) await RedirectToThisPage();
            // table / column / param
            try
            {
                DonationLot lote = await _data.GetByGenerecParams<DonationLot>("donation_lot", "cod", Cod_Lot);
                if (lote == null) await RedirectToThisPage();

                lote.Update(c.cod, lote.cod_pc);
                await _data.PostAsync<DonationLot>(lote, "donation_lot");

                await RedirectToThisPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<IActionResult> RedirectToThisPage()
        {
            //TempData["Email"] = JsonSerializer.Serialize(Email);
            return RedirectToPage();
        }
        private async Task<IActionResult> RedirectToLogin()
        {
            return RedirectToPage("/Login");
        }
    }
}

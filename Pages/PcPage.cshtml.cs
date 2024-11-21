using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System.Text.Json;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Entities;
using System.Runtime.CompilerServices;
using WebApp.Persistence.MySql;
using Microsoft.Extensions.WebEncoders.Testing;

namespace WebApp.Pages
{
    public class PcPageModel : PageModel
    {
        private readonly GetPcOutputSrv _pcSrv = new GetPcOutputSrv();  // Adicionei readonly
        private readonly UpdateDonationSrv _donationSrv = new UpdateDonationSrv(); // Adicionei readonly
        private readonly DonationsSrv _donationsSrv = new DonationsSrv();
        private MySqlPersistence _data = new MySqlPersistence();

        [BindProperty]
        public string Email { get; set; }  // Corrigido para maiúscula (convenção de C#)

        [BindProperty]
        public string Cod { get; set; }  // Corrigido para maiúscula

        [BindProperty]
        public string Cod_lot { get ; set; }

        [BindProperty]
        public DonationLot lote { get; set; }

        [BindProperty]
        public Dictionary<string, DonationLot> lotes { get; set; }

        [BindProperty]
        public string? Status { get; set; } // Já existente

        [BindProperty]
        public string? StatusMessage { get; set; } // Já existente

        public LinkedList<Donation>? Donations { get; set; }

        public async Task OnGet()
        {
            try
            {
                if (TempData == null) await RedirectToLogin();

                if (TempData["Email"] is string email)
                {
                    if (string.IsNullOrEmpty(email)) await RedirectToLogin();
                    Email = email.Trim('"').Replace("\\", "");
                   
                }
                if (TempData["Cod_lot"] is string cod_lot)
                {
                    if (!string.IsNullOrEmpty(cod_lot)) await RedirectToLogin();
                    Cod_lot = cod_lot;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await RedirectToLogin();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                Pc pc = new Pc();
                string pcCod = await pc.GetCod(Email);

                if (string.IsNullOrEmpty(pcCod))
                {
                    return Page();
                }

                await _donationSrv.Srv(Cod, pcCod, null, null);

                return await RedirectToThisPage();
            }
            catch (Exception ex)
            {
                return await RedirectToThisPage();
            }
        }

        public async Task OnPostCreateLot()
        {
            if (lotes == null) lotes = new Dictionary<string, DonationLot>();

            lote = new DonationLot();

            lote.Update(null, null);

            try
            {
                await _data.PostAsync<DonationLot>(lote, "donation_lot");

                Cod_lot = lote.cod;

                await RedirectToThisPage();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<IActionResult> RedirectToThisPage()
        {
            TempData["Email"] = JsonSerializer.Serialize(Email);
            TempData["Cod_lot"] = JsonSerializer.Serialize(Cod_lot);
            return RedirectToPage();
        }
        private async Task<IActionResult> RedirectToLogin()
        {
            return RedirectToPage("/Login");
        }
    }
}

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
using Org.BouncyCastle.Asn1;

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
        public string Cod_Pc { get; set; }

        [BindProperty]
        public string Cod_Lot { get; set; }

        [BindProperty]
        public DonationLot Lote { get; set; }

        [BindProperty]
        public bool HasMessage { get; set; }
        [BindProperty]
        public string Message { get; set; }


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
                if (TempData["CodLot"] is string cod_lot)
                {
                    if (!string.IsNullOrEmpty(cod_lot))
                        Cod_Lot = cod_lot.Trim('"').Replace("\\", "");
                }
                if (TempData["CodPc"] is string cod_pc)
                {
                    if (!string.IsNullOrEmpty(cod_pc))
                        Cod_Pc = cod_pc.Trim('"').Replace("\\", "");
                }
                if (TempData["Message"] is string message)
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        Message = message.Trim('"').Replace("\\", "");
                        HasMessage = true;
                    }
                    else
                    {
                        HasMessage = false;
                    }
                }

                if (string.IsNullOrEmpty(Cod_Pc) || !string.IsNullOrEmpty(Email))
                {
                    var pc = new Pc();
                    Cod_Pc = await pc.GetCod(Email);
                }

                DonationLot lote = await _data.GetByGenerecParams<DonationLot>("donation_lot", "cod_pc", Cod_Pc);
                if (lote != null) Cod_Lot = lote.cod;

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
                    return await RedirectToThisPage();
                }

                await _donationSrv.Srv(Cod, pcCod, null, null, "donation");

                return await RedirectToThisPage();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return await RedirectToThisPage();
            }
        }

        public async Task OnPostCreateLot()
        {
            if (!string.IsNullOrEmpty(Cod_Lot))
            {
                Message = $"Só é possível manter um lote por vez.";
                await RedirectToThisPage();
            }

            if (!string.IsNullOrEmpty(Cod_Lot))
            {
                Lote = await _data.GetByGenerecParams<DonationLot>("donation_lot", "cod", Cod_Lot);
            }
            if (Lote != null)
            {
                Cod_Lot = Lote.cod;
                await RedirectToThisPage();
            }

            Lote = new DonationLot();

            Lote.Update(null, Cod_Pc);

            try
            {
                await _data.PostAsync<DonationLot>(Lote, "donation_lot");

                Cod_Lot = Lote.cod;

                await RedirectToThisPage();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                await RedirectToThisPage();
            }
        }

        public async Task OnPostUpdateDonation()
        {
            if (string.IsNullOrEmpty(Cod_Lot))
            {
                Message = $"Não há lotes disponíveis";
            }
            // donationCod / PcCod / DonationLoteCod / Description
            else if (!string.IsNullOrEmpty(Cod) && !string.IsNullOrEmpty(Cod_Pc))
            {
                await _donationSrv.Srv(Cod, Cod_Pc, Cod_Lot, null, "donation");
            }
            await RedirectToThisPage();
        }

        private async Task<IActionResult> RedirectToThisPage()
        {
            TempData["CodPc"] = JsonSerializer.Serialize(Cod_Pc);
            TempData["Email"] = JsonSerializer.Serialize(Email);
            TempData["CodLot"] = JsonSerializer.Serialize(Cod_Lot);
            TempData["Message"] = JsonSerializer.Serialize(Message);
            return RedirectToPage();
        }
        private async Task<IActionResult> RedirectToLogin()
        {
            return RedirectToPage("/Login");
        }
    }
}

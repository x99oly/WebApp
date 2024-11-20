using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System.Text.Json;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Entities;

namespace WebApp.Pages
{
    public class PcPageModel : PageModel
    {
        private readonly GetPcOutputSrv _pcSrv = new GetPcOutputSrv();  // Adicionei readonly
        private readonly UpdateDonationSrv _donationSrv = new UpdateDonationSrv(); // Adicionei readonly

        [BindProperty]
        public string Email { get; set; }  // Corrigido para maiúscula (convenção de C#)

        [BindProperty]
        public string Cod { get; set; }  // Corrigido para maiúscula

        [BindProperty]
        public string? Status { get; set; } // Já existente

        [BindProperty]
        public string? StatusMessage { get; set; } // Já existente

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
                if (TempData["Status"] is string status)
                    Status = status;
                if (TempData["StatusMessage"] is string statusMessage)
                    StatusMessage = statusMessage;
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
                    //return RedirectToPage("/Login");
                    return Page();
                }

                await _donationSrv.Srv(Cod, pcCod, null, null);

                // Mensagem de sucesso
                TempData["Status"] = "success";
                TempData["StatusMessage"] = "Atualização realizada com sucesso!";
                TempData["Email"] = JsonSerializer.Serialize(Email);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                // Mensagem de erro genérico
                TempData["Status"] = "failure";
                TempData["StatusMessage"] = $"Erro durante a atualização: {ex.Message}";
                TempData["Email"] = JsonSerializer.Serialize(Email);
                return RedirectToPage();
            }
        }

        private async Task<IActionResult> RedirectToLogin()
        {
            return RedirectToPage("/Login");
        }
    }
}

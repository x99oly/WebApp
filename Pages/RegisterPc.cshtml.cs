using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.Entities;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Persistence.MySql;
using WebApp.Domain.DomainSrv;
using ReuseServer.Domain.DomainSrv;

namespace WebApp.Pages
{
    public class RegisterPcModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private MySqlPersistence _data = new MySqlPersistence();
        private DomainEmailSvc _message = new DomainEmailSvc();
        private RegisterPcSrv _pcSrv = new RegisterPcSrv();
        private UserInput _newUser;
        private PcInput _newPc;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string? Ddd { get; set; }

        [BindProperty]
        public string? Phone { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string CEP { get; set; }

        [BindProperty]
        public string STREET { get; set; }

        [BindProperty]
        public string? NUMBER { get; set; }

        [BindProperty]
        public string NEIGHBORHOOD { get; set; }

        [BindProperty]
        public string? COMPLEMENT { get; set; }

        [BindProperty]
        public string CITY { get; set; }

        [BindProperty]
        public string STATE { get; set; }

        public RegisterPcModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            try
            {

                _newUser = new UserInput(Name, Email, Ddd, Phone, Password, null);

                //var user = new User();
                //user.ConvertFrom(_newUser);

                //await _data.PostAsync(user, "users");

                _newPc = new PcInput(CEP, STREET, NEIGHBORHOOD, CITY, STATE, NUMBER, COMPLEMENT);

                await _pcSrv.Srv(_newUser, _newPc);

                //var pc = new Pc(user);
                //pc.ConvertFrom(_newPc);

                //await _data.PostAsync(pc, "pcs");

                var emailSrv = new GmailSvc();

                emailSrv.SendEmail(Email, _message.SuccessRegisterMessage);

                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                // Tratar exceções e dar uma mensagem de erro
                throw new Exception($"Erro ao processar o formulário: {ex.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Entities;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Persistence.MySql;
using WebApp.Domain.DomainSrv;
using ReuseServer.Domain.DomainSrv;
using System.ComponentModel.DataAnnotations;
using System.Linq;  // Adicionando LINQ para manipulação de erros

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
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [BindProperty]
        public string? Ddd { get; set; }

        [BindProperty]
        public string? Phone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string? Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A rua é obrigatória.")]
        public string STREET { get; set; }

        [BindProperty]
        public string? NUMBER { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "O bairro é obrigatório.")]
        public string NEIGHBORHOOD { get; set; }

        [BindProperty]
        public string? COMPLEMENT { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string CITY { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "O estado é obrigatório.")]
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
           
            if (!ModelState.IsValid)
            {
               
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"Erro de validação: {error.ErrorMessage}");
                }
                return Page();
            }

           
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(STATE)) 
            {
                ModelState.AddModelError(string.Empty, "Campos obrigatórios não preenchidos.");
                return Page();
            }

            if (string.IsNullOrEmpty(STATE) || STATE.Length < 2) 
            {
                ModelState.AddModelError(string.Empty, "Estado inválido.");
                return Page();
            }

            try
            {
              
                _newUser = new UserInput(Name, Email, Ddd, Phone, Password, null);
                _newPc = new PcInput(CEP, STREET, NEIGHBORHOOD, CITY, STATE, NUMBER, COMPLEMENT);

               
                await _pcSrv.Srv(_newUser, _newPc);
                _logger.LogInformation("Dados enviados para o serviço com sucesso.");

             
                var emailSrv = new GmailSvc();
                emailSrv.SendEmail(Email, _message.SuccessRegisterMessage);

                
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
          
                _logger.LogError($"Erro ao processar o formulário: {ex.Message}");
                _logger.LogError($"StackTrace: {ex.StackTrace}");
                _logger.LogError($"Estado recebido: {STATE}");  

            
                ModelState.AddModelError(string.Empty, $"Erro ao processar o formulário: {ex.Message}");
                return Page();
            }
        }
    }
}

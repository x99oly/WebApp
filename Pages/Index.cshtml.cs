using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReuseServer.Domain.DomainSrv;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private MySqlPersistence _mySql = new MySqlPersistence();
        
        private GmailSvc _email = new GmailSvc();
        private DomainEmailSvc _message = new DomainEmailSvc();

        public string CodUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Description))
            {
                ModelState.AddModelError(string.Empty, "Todos os campos são obrigatórios.");
                return Page();
            }

            UserInput newUser = new UserInput(Name, Email);

            try
            {
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a doação.");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao processar sua doação. Por favor, tente novamente.");
            }

            return Page();
        }
    }
}

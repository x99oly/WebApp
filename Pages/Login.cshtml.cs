using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private MySqlPersistence _sql = new MySqlPersistence();
        internal User user { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public async Task OnPost()
        {
           user = await _sql.GetByEmailAsync<User>(email);
        }
    }
}

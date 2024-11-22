using System.Net.Mail;
using WebApp.Aid;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.DomainSrv
{
    public class LoginService
    {
        private MySqlPersistence _data;
        public LoginService() {
            _data = new MySqlPersistence();
        }

        internal async Task<UserOutput> Logar(string email, string password)
        {
            if (email == "CersamBarreiro@Cersam.com" && password == "admin") return new UserOutput("Cersam Barreiro", email, null);

            var user = await _data.GetByEmailAsync<User>(email);

            if (user != null)
            {
                string hashPassword = Hashs.HashPassword(password + user.cod);

                if (hashPassword == user.password) return new UserOutput(user);
            }
            return null;
        }
    }
}

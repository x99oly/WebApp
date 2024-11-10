using WebApp.Aid;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.DomainSrv
{
    public class LoginService
    {
        private MySqlPersistence _mySql;
        public LoginService() {
            _mySql = new MySqlPersistence();
        }

        internal async Task<UserOutput> Logar(string email, string password)
        {
            var user = await _mySql.GetByEmailAsync<User>(email);

            if (user == null) throw new NullReferenceException(nameof(user));

            string hashPassword = Hashs.HashPassword(password + user.cod);

            if (hashPassword == user.password) return new UserOutput(user);

            return null;
        }
    }
}

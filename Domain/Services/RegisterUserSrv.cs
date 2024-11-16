using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.Services
{
    public class RegisterUserSrv
    {
        private MySqlPersistence _data;
        private User _user;

        public RegisterUserSrv()
        {
            _data = new MySqlPersistence();
        }

        public async Task Srv(UserInput newUser)
        {
            try
            {
                _user = new User(newUser);
            }
            catch (Exception ex) { throw new Exception("Não foi possível converter input em usuário."); }
            
            await _data.PostAsync<User>(_user, "users");
            
        }

    }
}

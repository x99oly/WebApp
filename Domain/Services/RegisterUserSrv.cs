using ReuseServer.Domain.DomainSrv;
using WebApp.Domain.DomainSrv;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Entities;
using WebApp.Domain.Interfaces;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.Services
{
    public class RegisterUserSrv
    {
        private MySqlPersistence _data;
        private User _user;

        private GmailSvc _email = new GmailSvc();
        private DomainEmailSvc _message = new DomainEmailSvc();

        public RegisterUserSrv()
        {
            _data = new MySqlPersistence();
        }

        internal async Task<UserOutput> Srv(UserInput newUser)
        {
            try
            {
                _user = await _data.GetByEmailAsync<User>(newUser.Email);
                if (_user != null) return _user.ConvertTo();

                _user = new User(newUser);
                await _data.PostAsync<User>(_user, "users");

                _email.SendEmail(_user.email, _message.SuccessRegisterMessage);

                return _user.ConvertTo();
            }
            catch (Exception e) { throw new Exception(e.Message); }

        }

    }
}

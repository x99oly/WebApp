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
               return newUser.Password == null ? await RegisterNewDonor(newUser) : await RegisterNewUser(newUser);
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        private async Task<UserOutput> RegisterNewUser(UserInput newUser)
        {
            _user = await _data.GetByEmailAsync<User>(newUser.Email);
            if (_user != null) return _user.ConvertTo();

            _user = new User(newUser);
            await _data.PostAsync<User>(_user, "users");

            _email.SendEmail(newUser.Email, _message.SuccessRegisterMessage);

            return _user.ConvertTo();
        }

        private async Task<UserOutput> RegisterNewDonor(UserInput newUser)
        {
            Donor donor = await _data.GetByEmailAsync<Donor>(newUser.Email, "donor");
            if (donor != null) return donor.ConvertTo();

            donor = new Donor(newUser);
            await _data.PostAsync<Donor>(donor, "donor");

            _email.SendEmail(newUser.Email, _message.SuccessRegisterMessage);

            return donor.ConvertTo();
        }

    }
}

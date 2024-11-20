using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.DomainSrv
{
    public class RegisterPcSrv
    {
        private MySqlPersistence _data;
        private User _user;
        private Pc _pc;

        public RegisterPcSrv()
        {
            _data = new MySqlPersistence();
        }

        public async Task Srv(UserInput user, PcInput pc)
        {
            try
            {
                _user = await _data.GetByEmailAsync<User>(user.Email);
                if (_user != null)
                {
                    if (!string.IsNullOrEmpty(_user.password)) throw new Exception("Usuário já cadastrado");
                }

                _user = new User(user);

                if (string.IsNullOrEmpty(_user.password)) throw new ArgumentNullException(nameof(_user.password));

                await _data.PostAsync<User>(_user, "users");

                _pc = await _data.GetByUserCodAsync<Pc>(_user.cod, "pcs");
                if (_pc == null)
                {
                    _pc = new Pc(_user);
                    _pc.ConvertFrom(pc);

                    await _data.PostAsync<Pc>(_pc, "pcs");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.DomainSrv
{
    public class DonationsSrv
    {
        public MySqlPersistence _data;
        public DonationsSrv()
        {
            _data = new MySqlPersistence();
        }

        public async Task<LinkedList<Donation>> GetAllDonationFromPcByEmail(string email)
        {
            User? user = await _data.GetByGenerecParams<User>("users", "email", email);
            if (user == null) throw new NullReferenceException(nameof(user));

            Pc? pc = await _data.GetByGenerecParams<Pc>("pcs", "cod_user", user.cod);
            if (pc == null) throw new NullReferenceException(nameof(pc));

            return new LinkedList<Donation>(await _data.GetListByGenericParams<Donation>("donation", "cod_pc", pc.cod));
        }



    }
}

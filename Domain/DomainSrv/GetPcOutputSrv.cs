using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.DomainSrv
{
    public class GetPcOutputSrv
    {
        private MySqlPersistence _data;

        public GetPcOutputSrv()
        {
            _data = new MySqlPersistence();
        }

        public async Task<PcOutput> Srv(string email)
        {
            try
            {
                User user = await _data.GetByEmailAsync<User>(email);
                return new PcOutput(await _data.GetByEmailAsync<Pc>(email, user.cod, "pcs"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

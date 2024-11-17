using Microsoft.Extensions.Logging.Abstractions;
using MySql.Data.MySqlClient.Authentication;
using WebApp.Domain.Entities;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.DomainSrv
{
    public class UpdateDonationSrv
    {
        private MySqlPersistence _data;
        private Donation _donation;

        public UpdateDonationSrv()
        {
            _data = new MySqlPersistence();
        }

        public async Task Srv(string donationCod, string? pcCod, string? cersamCod, string? description)
        {
            try
            {
                _donation = await _data.GetByCodAsync<Donation>(donationCod, "donation");
                if (_donation == null) { throw new ArgumentNullException(nameof(donationCod)); }

                _donation.UpdateEntity(pcCod, cersamCod, description);

                await _data.PostAsync<Donation>(_donation, "donation");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}

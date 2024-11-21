using WebApp.Aid;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace WebApp.Domain.Entities
{
    public class DonationLot
    {
        [Key]
        public string cod { get; set; }

        public string? cod_pc { get; set; }
        public string? cod_cersam { get; set; }
        public DateTime date { get; set; }

        public DonationLot() { }

        public void Update(string? cod_cersam, string? cod_pc)
        {
            if(cod == null) cod = MyString.BuildRandomString(4);
            if(date == DateTime.MinValue) date = DateTime.Now;

            this.cod_pc = cod_pc;
            this.cod_cersam = cod_cersam;

        }

    }
}

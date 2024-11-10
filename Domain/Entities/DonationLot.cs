using WebApp.Aid;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Domain.Entities
{
    internal class DonationLot
    {
        [Key]
        public string cod { get; set; }

        [ForeignKey("Cersam")]
        public string? cod_cersam { get; set; }
        public DateTime date { get; set; }

        public virtual Cersam? cersam { get; set; }
    }
}

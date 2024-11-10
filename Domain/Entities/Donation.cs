using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Entities
{
    internal class Donation
    {
        [Key]
        public string cod { get; set; }

        [Required]
        [ForeignKey("User")]
        public string cod_user { get; set; }

        [Required]
        [ForeignKey("Pc")]
        public string cod_pc { get; set; }

        [ForeignKey("Cersam")]
        public string? cod_cersam { get; set; }

        public DateTime date { get; set; }
        public string description { get; set; }
        public bool finished { get; set; }

        // Propriedades de navegação
        public virtual User user { get; set; }
        public virtual Pc pc { get; set; }
    }
}

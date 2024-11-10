using WebApp.Aid;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.Enums;

namespace WebApp.Domain.Entities
{
    internal class Pc 
    {
        [Key]
        public string cod { get; set; }

        [ForeignKey("User")]
        public string user_cod { get; set; }

        [Required]
        public string cep { get; set; }

        [Required]
        public string street { get; set; }
        public string? number { get; set; }

        [Required]
        public string neighborhood { get; set; }
        public string? complement { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string state { get; set; }

        public Pc() { }
        
        public Pc(User user)
        {
            if (user.cod == null) { throw new InvalidOperationException(); }
            user_cod = user.cod;
            cod = MyString.BuildRandomString(null);
        }

        public void ConvertFrom(PcInput input)
        {
            if (input != null)
            {
                cep = input.CEP;
                street = input.STREET;
                number = input.NUMBER ?? null;
                neighborhood = input.NEIGHBORHOOD;
                city = input.CITY;
                state = input.STATE;
                complement = input.COMPLEMENT ?? null;
            }
        }

        public PcOutput ComvertTo()
        {
            return new PcOutput(this);
        }
    }
}
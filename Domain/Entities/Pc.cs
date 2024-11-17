using WebApp.Aid;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.DTOs.Outputs;
using WebApp.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.Enums;
using WebApp.Persistence.MySql;

namespace WebApp.Domain.Entities
{
    public class Pc 
    {
        [Key]
        public string cod { get; set; }

        [ForeignKey("User")]
        public string cod_user { get; set; }

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
            cod_user = user.cod;
            cod = MyString.BuildRandomString(null);
        }

        /// <summary>
        /// Para conseguir o código do ponto de coleta usando email do usuário associado. Retorna null se email não existir.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<string> GetCod(string email)
        {
            MySqlPersistence mysql = new MySqlPersistence();

            User user = await mysql.GetByEmailAsync<User>(email);
            if (user == null) return null;

            Pc pc = await mysql.GetByUserCodAsync<Pc>(user.cod, "pcs");
            if (pc == null) return null;

            return pc.cod;
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
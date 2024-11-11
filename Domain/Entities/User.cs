
using System.ComponentModel.DataAnnotations;
using WebApp.Aid;
using WebApp.Domain.DTOs.Inputs;
using WebApp.Domain.DTOs.Outputs;

namespace WebApp.Domain.Entities
{
    internal class User
    {
        [Key]
        public string? cod { get; private set; }

        [Required]
        public string name { get; private set; }

        [Required]
        public string email { get; private set; }
        public string? ddd { get; private set; }
        public string? phone { get; private set; }
        public string? password { get; private set; }
        public byte[]? imgBin { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="User"/> usando os dados fornecidos no <see cref="UserInput"/>.
        /// </summary>
        /// <param name="user">O objeto de entrada que contém os dados do usuário.</param>
        public User() { }

        public User(string? cod, string name, string email, string? ddd = null, string? phone = null, string? password = null, byte[]? imgBin = null)
        {
            this.name = name;
            this.email = email;
            this.ddd = ddd;
            this.phone = phone;
            this.imgBin = imgBin;

            this.cod = cod ?? MyString.BuildRandomString(null);
            if (password != null)
            {
                var passwordPlusSalt = password + this.cod;
                this.password = Hashs.HashPassword(passwordPlusSalt);
            }
        }

        private bool IsUserInputValid(UserInput input)
        {
            return input.Name != null && input.Email != null;
        }

        public void ConvertFrom(UserInput user)
        {
            if(!IsUserInputValid(user)) throw new Exception("Nome não pode ser nulo");

            name = user.Name;
            email = user.Email;
            ddd = user.Ddd ?? null;
            phone = user.Phone ?? null;
            imgBin = user.ImgBin ?? null;

            cod = MyString.BuildRandomString(null);
            if (user.Password != null)
            {
                var passwordPlusSalt = user.Password + cod;
                password = Hashs.HashPassword(passwordPlusSalt);
            }
        }

        public UserOutput ConvertTo()
        {
            return new UserOutput(this);
        }

        public override string ToString()
        {
            return $"Nome: {name}, Código identificador: {cod}.";
        }
    }
}

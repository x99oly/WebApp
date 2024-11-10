using System;
using System.Text.Json;

namespace WebApp.Domain.DTOs.Inputs
{
    /// <summary>
    /// Representa os dados de entrada para um usuário, incluindo validação de email e nome.
    /// </summary>
    public class UserInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Ddd { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public byte[]? ImgBin { get; set; }

        public UserInput() { }

        /// <summary>
        /// Construtor que cria uma instância de <see cref="UserInput"/> a partir de uma string JSON.
        /// </summary>
        /// <param name="jsonstring">A string JSON a ser convertida.</param>
        public UserInput(string jsonstring)
        {
            var newUser = CastFromJson(jsonstring);
            if (newUser == null)
            {
                throw new ArgumentException("A conversão de JSON para UserInput falhou: a string JSON não pôde ser desserializada.", nameof(jsonstring));
            }

            if (!isUserInputValid(newUser))
            {
                throw new ArgumentException("UserInput falhou na criação: Nem todos os dados estavam corretos.");
            }

            Name = newUser.Name;
            Email = newUser.Email;
            Ddd = newUser.Ddd;
            Phone = newUser.Phone;
            Password = newUser.Password;
            ImgBin = newUser.ImgBin;
        }

        public UserInput(string name, string email, string? ddd, string? phone, string? password, byte[]? imgBin)
        {
            Name = name;
            Email = email;
            Ddd = ddd;
            Phone = phone;
            Password = password;
            ImgBin = imgBin;

           // if (!isUserInputValid(this)) throw new Exception("Não foi possível criar usuário.");
        }



        /// <summary>
        /// Converte uma string JSON em uma instância da classe <see cref="UserInput"/>.
        /// </summary>
        /// <param name="jsonstring">A string JSON a ser convertida.</param>
        /// <returns>Uma nova instância de <see cref="UserInput"/> preenchida com os dados do JSON.</returns>
        private UserInput? CastFromJson(string jsonstring)
        {
            if (string.IsNullOrWhiteSpace(jsonstring))
            {
                throw new ArgumentException("A string JSON não pode ser nula ou vazia.", nameof(jsonstring));
            }

            return JsonSerializer.Deserialize<UserInput>(jsonstring);
        }

        private bool isUserInputValid(UserInput user)
        {
            // Validação de email
            if (!IsAnValidEmail(user.Email))
            {
                throw new ArgumentException("Email inválido fornecido.", nameof(user.Email));
            }

            // Validação de nome
            if (!isAnValidName(user.Name))
            {
                throw new ArgumentException("Nome inválido fornecido: deve ter pelo menos 2 caracteres.", nameof(user.Name));
            }

            return true;
        }

        private bool isAnValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 2;
        }

        private bool IsAnValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email não pode ser nulo ou vazio.");

            var parts = email.Split('@');

            if (parts.Length != 2)
            {
                throw new FormatException("O email deve conter exatamente um '@'.");
            }

            if (string.IsNullOrWhiteSpace(parts[0]) || parts[1].Length < 3)
            {
                throw new FormatException("O nome do usuário e o domínio do email devem ser válidos.");
            }

            if (!parts[1].Contains(".") || parts[1].EndsWith("."))
            {
                throw new FormatException("O domínio do email deve conter pelo menos um '.' válido.");
            }

            var domainParts = parts[1].Split('.');
            if (domainParts.Length < 2 || string.IsNullOrWhiteSpace(domainParts[^1]))
            {
                throw new FormatException("O domínio deve ter uma parte final válida.");
            }

            return true;
        }
    }
}

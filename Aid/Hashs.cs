using System.Security.Cryptography;
using System.Text;

namespace WebApp.Aid
{
    public class Hashs
    {
        /// <summary>
        /// Gera um hash SHA256 para a senha fornecida.
        /// </summary>
        /// <param name="password">A senha a ser hashada. Pode ser <c>null</c>, neste caso, o método retorna <c>null</c>.</param>
        /// <returns>
        /// Retorna a representação em string hexadecimal do hash SHA256 da senha. 
        /// Retorna <c>null</c> se a senha fornecida for <c>null</c>.
        /// </returns>
        public static string? HashPassword(string password)
        {
            if (password == null) return null;

            using (var hash = SHA256.Create())
            {
                // Converte a senha em bytes e calcula o hash
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                var sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("X2")); // Formato hexadecimal em maiúsculas
                }

                return sb.ToString();
            }
        }

    }
}

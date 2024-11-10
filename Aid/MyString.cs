using System.Text;

namespace WebApp.Aid
{
    internal class MyString
    {
        /// <summary>
        /// Gera uma string aleatória de caracteres com um comprimento especificado, com tamanho padrão de 10 caracteres.
        /// </summary>
        /// <param name="length">O comprimento da string aleatória a ser gerada. Se for nulo ou menor que 1, o comprimento padrão será 10.</param>
        /// <returns>Uma string aleatória composta por letras, números e caracteres especiais.</returns>
        public static string BuildRandomString(int? length)
        {
            if (length == null || length < 0) length = 10;

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&*+:?";
            var sb = new StringBuilder();
            var r = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[r.Next(0, chars.Length)]);
            }
            return sb.ToString();
        }
    }
}

using System.Text;
using WebApp.Domain.Entities;

namespace WebApp.Domain.DTOs.Outputs
{
    public class UserOutput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[]? ImgBin { get; set; }

        public UserOutput() { }
        public UserOutput(User user)
        {
            Name = user.name;
            Email = FormatEmail(user.email);
            ImgBin = user.imgBin ?? null;
        }

        public UserOutput(string nome, string email, byte[]? imgbin)
        {
            Name = nome;
            Email = FormatEmail(email);
            ImgBin = imgbin ?? null;
        }

        private string FormatEmail(string email)
        {
            if (email == null || !email.Contains("@")) return null;

            var sb = new StringBuilder();
            sb.Append(email[0]);
            sb.Append(email[1]);

            for (int i = 2; i < email.Length; i++)
            {
                if (email[i] == '@') break;

                sb.Append("*");
            }
            sb.Append(email.Substring(sb.Length));

            return sb.ToString();
        }
    }
}

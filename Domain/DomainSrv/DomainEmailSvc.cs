using System.Net.Mail;
using System.Net;
using System.Text.Json;

namespace ReuseServer.Domain.DomainSrv
{
    public class DomainEmailSvc
    {
        internal string _domainEmail { get; }
        internal string _domainPassword { get; }

        public DomainEmailSvc()
        {
            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Credentials.json");
            string jsonString = File.ReadAllText(jsonFilePath);

            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                _domainEmail = doc.RootElement.GetProperty("EmailSettings").GetProperty("DomainEmail").GetString();
                _domainPassword = doc.RootElement.GetProperty("EmailSettings").GetProperty("DomainPassword").GetString();
            }
        }

        public SmtpClient ConfigSmtp()
        {
            return new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_domainEmail, _domainPassword),
                EnableSsl = true,
            };
        }

        public MailMessage SuccessRegisterMessage()
        {
            return new MailMessage()
            {
                From = new MailAddress(_domainEmail),
                Subject = "Reuse - Cadastro Realizado!",
                Body = "Cadastro realizado com sucesso! Estamos muito felizes em ter você conosco.",
                IsBodyHtml = false,
            };
        }
    }
}

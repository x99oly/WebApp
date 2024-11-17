using System.Net.Mail;
using System.Net;
using System.Text.Json;
using WebApp.Domain.DomainSrv;

namespace ReuseServer.Domain.DomainSrv
{
    public class DomainEmailSvc
    {
        internal string _domainEmail { get; }
        internal string _domainPassword { get; }

        private GetCredentials _credentials;

        public DomainEmailSvc()
        {
            _credentials = new GetCredentials();
            _domainEmail = _credentials.domainEmail;
            _domainPassword = _credentials.domainPassword;
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

        public MailMessage SuccessDonationMessage()
        {
            return new MailMessage()
            {
                From = new MailAddress(_domainEmail),
                Subject = "Reuse - Doação cadastrado com sucesso!",
                Body = $"Sua doação foi registrada com sucesso. Entregue-a em um ponto de coleta para confirmação.",
                IsBodyHtml = false,
            };
        }

        public MailMessage SuccesssPcReceived()
        {
            return new MailMessage()
            {
                From = new MailAddress(_domainEmail),
                Subject = "Reuse - Doação entregue com sucesso!",
                Body = $"Sua doação foi entregue no ponto de coleta com sucesso. Avisaremos assim que for entregue ao Cersam.\nMuito obrigado! <3",
                IsBodyHtml = false,
            };
        }

        public MailMessage SuccesssCersamReceived()
        {
            return new MailMessage()
            {
                From = new MailAddress(_domainEmail),
                Subject = "Reuse - Doação entregue com sucesso!",
                Body = $"Sua doação foi entregue ao CERSAM.\nMuito obrigado! <3",
                IsBodyHtml = false,
            };
        }
    }
}

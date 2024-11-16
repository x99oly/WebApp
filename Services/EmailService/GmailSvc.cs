using ReuseServer.Domain.DomainSrv;
using System.Net;
using System.Net.Mail;

namespace WebApp.Domain.DomainSrv
{
    public class GmailSvc
    {
        private DomainEmailSvc config;

        public GmailSvc()
        {
            config = new DomainEmailSvc();
        }
        public async Task SendEmail(string destinatario, Func<MailMessage> func)
        {
            try
            {
                // Configurações do cliente SMTP
                var smtpClient = config.ConfigSmtp();

                // Criando mensagem
                var mailMessage = func();
                mailMessage.To.Add(destinatario);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex) { throw new Exception("Erro ao enviar email: " + ex.Message); }
        }

    }
}

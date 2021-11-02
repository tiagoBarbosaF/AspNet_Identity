using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ByteBank.Forum.Identity
{
    public class EmailService : IIdentityMessageService
    {
        private readonly string EMAIL_ORIGEM = ConfigurationManager.AppSettings["emailServico:email_remetente"];
        private readonly string EMAIL_SENHA = ConfigurationManager.AppSettings["emailServico:email_senha"];

        public async Task SendAsync(IdentityMessage message)
        {
            using (var mensagemDeEmail = new MailMessage())
            {
                mensagemDeEmail.From = new MailAddress(EMAIL_ORIGEM);

                mensagemDeEmail.Subject = message.Subject;
                mensagemDeEmail.To.Add(message.Destination);
                mensagemDeEmail.Body = message.Body;
                
                // SMTP - Simple Mail Transport Protocol
                using (var smtpCliente = new SmtpClient())
                {
                    smtpCliente.UseDefaultCredentials = true;
                    smtpCliente.Credentials = new NetworkCredential(EMAIL_ORIGEM, EMAIL_SENHA);
                    smtpCliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpCliente.Host = "smtp.gmail.com";
                    smtpCliente.Port = 465;
                    smtpCliente.EnableSsl = true;

                    smtpCliente.Timeout = 20000;

                    await smtpCliente.SendMailAsync(mensagemDeEmail);
                }
            }
        }
    }
}
using CoreApp.Common.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreApp.Common.Helpers
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(SendMailModel model)
        {
            SmtpClient client = new SmtpClient(model.SmtpServer);
            client.UseDefaultCredentials = false;
            client.EnableSsl = model.EnableSsl;
            client.Credentials = new NetworkCredential(model.SmtpEmail, model.SmtpPassword);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(model.Sender);
            foreach (var reciver in model.Recivers)
            {
                mailMessage.To.Add(reciver);
            }
            mailMessage.Body = model.Body;
            mailMessage.Subject = model.Subject;

            return client.SendMailAsync(mailMessage);
        }

        public Task SendEmailConfirmationAsync(string email, string link)
        {
            return Task.CompletedTask;
        }
    }
}

namespace CoreApp.Common.Models
{
    public class SendMailModel
    {
        public string[] Recivers { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpEmail { get; set; }
        public string SmtpPassword { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Sender { get; set; }
        public bool EnableSsl { get; set; }
    }
}

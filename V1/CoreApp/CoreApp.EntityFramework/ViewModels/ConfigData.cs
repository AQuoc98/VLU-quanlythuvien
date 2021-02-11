namespace CoreApp.EntityFramework.ViewModels
{
    public class ConfigData
    {
        public string SMTP_SERVER { get; set; }
        public string SMTP_EMAIL { get; set; }
        public string EMAIL_SENDER { get; set; }
        public string SMTP_PASSWORD { get; set; }
        public string FORGOT_PASSWORD_EMAIL_SUBJECT { get; set; }
        public string FORGOT_PASSWORD_EMAIL_BODY { get; set; }
        public string CONFIRM_CODE_SENDER { get; set; }
        public string RULE { get; set; }
    }
}

using CoreApp.Common.Models;
using System.Threading.Tasks;

namespace CoreApp.Common.Helpers
{
    public interface IEmailSender
    {
        Task SendEmailAsync(SendMailModel model);
        Task SendEmailConfirmationAsync(string email, string link);
    }
}

using CoreApp.Common._3rd.OneSignal.Providers;
using CoreApp.Common._3rd.OneSignal.Templates;
using System.Threading.Tasks;

namespace CoreApp.Common._3rd.OneSignal.Workers
{
    public class Sender
    {
        /// <summary>
        /// Returns successful statement
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SendNotification(Notification notification)
        {
            var response = await ClientManager.Post<object, Notification>(notification, Configuration.URL_Notification_POST, notification.app);
            return response != null;
        }
    }
}

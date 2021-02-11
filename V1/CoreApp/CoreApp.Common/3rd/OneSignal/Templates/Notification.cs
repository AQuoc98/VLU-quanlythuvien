using CoreApp.Common._3rd.OneSignal.Workers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApp.Common._3rd.OneSignal.Templates
{
    public class Notification
    {
        /// <summary>
        /// Send App
        /// </summary>
        public string app { get; set; }
        /// <summary>
        /// To send for all, single topic, device group, single token
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// To send for multiple token
        /// </summary>
        public List<string> registration_ids { get; set; }

        /// <summary>
        /// Set values are normal or high. On iOS, these correspond to APNs priorities 5 and 10.
        /// </summary>
        public string priority { get; set; }

        /// <summary>
        /// Optional. Set for application filtering
        /// </summary>
        public string restricted_package_name { get; set; }

        /// <summary>
        /// Any data to send
        /// </summary>
        public Object data { get; set; }

        /// <summary>
        /// This parameter specifies the predefined, user-visible key-value pairs of the notification payload. 
        /// </summary>
        public NotificationMessage notification { get; set; }

        public Notification(List<string> registrationIds, string title, string message, string deeplink, string app, Object data)
        {
            this.priority = "high";
            this.registration_ids = registrationIds;
            this.app = app;
            this.data = data;
            this.notification = new NotificationMessage
            {
                title = title,
                body = message,
                click_action = deeplink,
                color = "#87CEEB", // Skyblue
                icon = "ic_stat_onesignal_default.png",
                sound = "default"
            };
        }

        /// <summary>
        /// returns proccess successful statement
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Send()
        {
            return await Sender.SendNotification(this);
        }
    }

    public class NotificationMessage
    {
        /// <summary>
        /// The notification's title.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// The notification's body text.
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// The notification's icon.
        /// This param just for android.
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// The sound to play when the device receives the notification.
        /// Set values are 'default' or filename in resource with path '/res/raw/'
        /// </summary>
        public string sound { get; set; }

        /// <summary>
        /// The notification's icon color, expressed in #rrggbb format.
        /// This param just for android.
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// The action associated with a user click on the notification.
        /// </summary>
        public string click_action { get; set; }
    }
}

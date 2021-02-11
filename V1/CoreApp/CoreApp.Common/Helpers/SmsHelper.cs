using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Common.Helpers
{
    public static class SmsHelper
    {
        private static readonly string _accessKey = "b73f5ac77e1b864963f02bee734eacaf";
        private static readonly string _accountSID = "ACde18ca75fb943a831b2415105679b2b3";
        private static readonly string _countryCode = "+84";
        private static readonly string _smsServerApi = "https://api.twilio.com/2010-04-01/Accounts/ACde18ca75fb943a831b2415105679b2b3/Messages.json";

        public static async Task<string> SendSmsAsync(string sender, string recipient, string message)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), _smsServerApi))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_accountSID}:{_accessKey}"));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    var contentList = new List<string>();
                    contentList.Add($"Body={Uri.EscapeDataString(message)}");
                    contentList.Add($"From={Uri.EscapeDataString(sender)}");
                    contentList.Add($"To={Uri.EscapeDataString(ConvertToCountryCode(recipient))}");
                    request.Content = new StringContent(string.Join("&", contentList), Encoding.UTF8, "application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
                }
            }
        }

        private static string ConvertToCountryCode(string recipient)
        {
            if (recipient.Contains(_countryCode))
                return recipient;
            return $"{_countryCode}{recipient.Substring(1)}";
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Common._3rd.OneSignal.Providers
{
    public class ClientManager
    {
        /// <summary>
        /// Use this dictionary to add something to your requqest's header
        /// </summary>
        static Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            //{"Content-Type","application/json" },
        };

        public static HttpClient GetClient(string app)
        {

            HttpClient client = new HttpClient();
            foreach (var item in Headers)
                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            if (app.ToUpper().Equals(Configuration.APP_SCHOOL))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", $"key={Configuration.FIREBASE_SCHOOL_LEGACY_KEY}");
            }
            if (app.ToUpper().Equals(Configuration.APP_PARENT))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", $"key={Configuration.FIREBASE_PARENT_LEGACY_KEY}");
            }
            return client;
        }

        /// <summary>
        /// Post method
        /// </summary>
        /// <typeparam name="TResult">Result to deserialize object type</typeparam>
        /// <typeparam name="TPost">Type to be posted</typeparam>
        /// <param name="value">post value to be serialized</param>
        /// <param name="postUrl">Post url</param>
        public static async Task<TResult> Post<TResult, TPost>(TPost value, string postUrl, string app)
        {
            var response = await ClientManager.GetClient(app).PostAsync(postUrl,
                              new StringContent(JsonConvert.SerializeObject(value, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                              Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var mobileResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(mobileResult);
            return result;

        }



        /// <summary>
        /// Get method
        /// </summary>
        /// <typeparam name="TResult">Result to deserialize object type</typeparam>
        /// <param name="url">url to post</param>
        public static async Task<TResult> Get<TResult>(string url, string app)
        {
            var response = await ClientManager.GetClient(app).GetAsync(url).ConfigureAwait(false);
            var mobileResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(mobileResult);
            return result;

        }
    }
}

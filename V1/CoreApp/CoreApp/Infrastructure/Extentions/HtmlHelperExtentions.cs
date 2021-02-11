using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace CoreApp.Infrastructure.Extentions
{
    public static class HtmlHelperExtentions
    {
        /// <summary>
        /// Load Angular Script
        /// </summary>
        /// <param name="helper"></param>
        /// <returns>String Html</returns>
        public static IHtmlContent LoadReactApp(this IHtmlHelper helper)
        {
            var html =
                File.ReadAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/frontend", "index.html"));

            // Update url
            //html = html.Replace(@"/assets", "/frontend/assets").Replace(@"/vendors", "/frontend/vendors");

            return helper.Raw(html);
        }

        /// <summary>
        /// Convert to Json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="includeNull"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj, bool includeNull = true)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] { new StringEnumConverter() },
                NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IHtmlContent JsonFor<T>(this IHtmlHelper helper, T obj)
        {
            return helper.Raw(obj.ToJson());
        }
    }
}

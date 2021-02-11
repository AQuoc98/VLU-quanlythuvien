using Newtonsoft.Json;

namespace CoreApp.Common.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize(object input)
        {
            var setting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(input, setting);
        }

        public static T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}

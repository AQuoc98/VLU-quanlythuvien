using System.Linq;
using System.Xml.Linq;

namespace CoreApp.Common.Helpers
{
    public static class XmlHelper
    {
        public static string GetValue(string xmlString, string key)
        {
            var xDoc = XDocument.Parse(xmlString);

            var value = xDoc.Descendants(key).FirstOrDefault()?.Value ?? string.Empty;

            return value;
        }
    }
}

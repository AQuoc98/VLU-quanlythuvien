using System.Collections.Generic;

namespace CoreApp.Common._3rd.OneSignal.Templates
{
    public class LocalizatedContent : Dictionary<string, string>
    {
        public LocalizatedContent()
        {

        }
        public LocalizatedContent(string _vn)
        {
            this.Add("en", _vn);
            this.Add("vn", _vn);
        }
        public static implicit operator LocalizatedContent(string value)
        {
            return new LocalizatedContent(value);
        }
    }
}

using System;

namespace CoreApp.Common.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Convert string to camel case type
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ConverToCamelCase(string inputStr)
        {
            return Char.ToLowerInvariant(inputStr[0]) + inputStr.Substring(1);
        }

        public static string ConvertToVnPhone(string strInput)
        {
            if (strInput.Contains("+84")) return strInput.Replace("+84", "0");
            return strInput;
        }

        public static string ConvertSrcThumb(string src)
        {
            if (!string.IsNullOrEmpty(src))
            {
                return src.Replace(".jpg", ".thumb.jpg").Replace(".png", ".thumb.jpg");
            }
            return src;
        }
    }
}

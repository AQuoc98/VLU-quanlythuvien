using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreApp.Common.Helpers
{
    public static class UrlHelper
    {
        /// <summary>
        /// Generate slug
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GenerateSlug(string inputStr, int maxLength = 80)
        {
            string slug = RemoveDiacritics(inputStr).ToLower();
            // invalid chars           
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            // cut and trim 
            slug = slug.Substring(0, slug.Length <= maxLength ? slug.Length : maxLength).Trim();
            slug = Regex.Replace(slug, @"\s", "-"); // hyphens   
            return slug;
        }

        /// <summary>
        /// Normalize text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveDiacritics(string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return s.Normalize(NormalizationForm.FormC);
        }
    }
}

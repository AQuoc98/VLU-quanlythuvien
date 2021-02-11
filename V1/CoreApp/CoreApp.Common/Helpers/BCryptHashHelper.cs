
using System;
using System.Text;

namespace CoreApp.Common.Helpers
{
    public static class BCryptHashHelper
    {
        private static readonly int Cost = 12;

        public static string ComputeHash(string inputKey)
        {
            return BCrypt.Net.BCrypt.HashPassword(inputKey, Cost);
        }

        public static bool ValidateHash(string inputKey, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputKey, correctHash);
        }
    }
}

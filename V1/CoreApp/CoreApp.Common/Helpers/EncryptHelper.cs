using System;
using System.Text;

namespace CoreApp.Common.Helpers
{
    public static class EncryptHelper
    {
        public static string Hash(string input)
        {
            return BCryptHashHelper.ComputeHash(input);
        }

        public static bool ValidateHash(string input, string correctHash)
        {
            return correctHash == null || string.IsNullOrEmpty(input) ? false : BCryptHashHelper.ValidateHash(input, correctHash);
        }

        public static string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        public static string StringEncrypt(string inputStr)
        {
            return StringCipher.Encrypt(inputStr);
        }

        public static string StringDecrypt(string strEncrypted)
        {
            return StringCipher.Decrypt(strEncrypted);
        }

        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}

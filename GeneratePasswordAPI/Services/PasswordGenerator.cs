using System;
using System.Security.Cryptography;

namespace GeneratePasswordAPI.Services
{
    public class PasswordGenerator
    {
        private const string UpperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string DigitChars = "0123456789";
        private const string SpecialChars = "!@#$%^&*()-_+=<>?";

        public static string GeneratePassword(int length, bool includeSpecialChars)
        {
            
            string chars = UpperCaseChars + LowerCaseChars + DigitChars;
            if (includeSpecialChars)
            {
                chars += SpecialChars;
            }

            byte[] data = new byte[length];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = chars[data[i] % chars.Length];
            }

            Shuffle(password);

            return new string(password);
        }

        private static void Shuffle<T>(T[] array)
        {
            Random random = new Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = random.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static string GetUpperCaseChars() => UpperCaseChars;
        public static string GetLowerCaseChars() => LowerCaseChars;
        public static string GetDigitChars() => DigitChars;
        public static string GetSpecialChars() => SpecialChars;
    }
}

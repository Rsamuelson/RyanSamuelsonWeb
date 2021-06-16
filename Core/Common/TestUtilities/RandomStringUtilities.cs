using System;
using System.Linq;

namespace Common.TestUtilities
{
    public static class RandomStringUtilities
    {
        private const string AlphaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericCharacters = "0123456789";

        public static string RandomAlphaNumericString(int length = 10)
        {
            var random = new Random();

            return new string(
                Enumerable.Repeat(AlphaCharacters + NumericCharacters, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
            );
        }

        public static string RandomAlphaString(int length = 10)
        {
            var random = new Random();

            return new string(
                Enumerable.Repeat(AlphaCharacters, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
            );
        }

        public static string RandomNumericString(int length = 10)
        {
            var random = new Random();

            return new string(
                Enumerable.Repeat(NumericCharacters, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
            );
        }
    }
}

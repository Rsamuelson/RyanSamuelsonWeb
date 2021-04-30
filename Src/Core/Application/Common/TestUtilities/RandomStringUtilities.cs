using System;
using System.Linq;

namespace Application.Common.TestUtilties
{
    public static class RandomStringUtilities
    {
        private const string AlphaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericCharacters = "0123456789";

        public static string RandomAlphaNumericString(int length)
        {
            var random = new Random();

            return new string(
                Enumerable.Repeat(AlphaCharacters + NumericCharacters, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
            );
        }

        public static string RandomAlphaString(int length)
        {
            var random = new Random();

            return new string(
                Enumerable.Repeat(AlphaCharacters, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
            );
        }

        public static string RandomNumericString(int length)
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

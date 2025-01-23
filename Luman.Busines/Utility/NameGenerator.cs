using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Utility
{
    public static class NameGenerator
    {
        /// <summary>
        /// Generates a random short name with the specified length.
        /// </summary>
        /// <param name="length">The length of the generated name.</param>
        /// <returns>A randomly generated string with the specified length.</returns>
        public static string GenerateShortName(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}

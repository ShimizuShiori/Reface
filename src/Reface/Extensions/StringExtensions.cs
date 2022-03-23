using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Reface
{
    public static class StringExtensions
    {
        public static string Join<T>(this IEnumerable<T> list, string joiner, Func<T, string> map)
        {
            if (!list.Any()) return "";
            if (list.Count() == 1) return map(list.First());
            return list.Select(x => map(x)).Aggregate((a, b) => $"{a}{joiner}{b}");
        }


        /// <summary>
        /// a word means it begin with upper case letter
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<string> SplitToWords(this string text)
        {
            List<string> result = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (var c in text)
            {
                if (!Char.IsUpper(c) || sb.Length == 0)
                {
                    sb.Append(c);
                    continue;
                }

                result.Add(sb.ToString());
                sb.Clear();
                sb.Append(c);
            }
            if (sb.Length != 0)
                result.Add(sb.ToString());
            return result;
        }

        public static string ToMd5(this string value)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();


            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));


            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();


            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }


            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}

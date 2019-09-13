using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace senrenbanka.murasame.qqbot.Resources.Primitive
{
    public static class String
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static byte[] GetBytes(this string source, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetBytes(source);
        }

        public static string GetString(this byte[] source, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetString(source);
        }

        public static string MatchFirst(this string source, string regex)
        {
            var matcher = Regex.Match(source, regex);
            return matcher.Groups[1].Value;
        }

        public static bool AnyIsNullOrEmpty(params string[] sources)
        {
            return sources.Any(IsNullOrEmpty);
        }

        public static bool IsNumber(this string source)
        {
            return int.TryParse(source, out _);
        }
    }
}
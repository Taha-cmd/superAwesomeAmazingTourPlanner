using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class StringExtensions
    {
        public static string Reverse(this string str) => new string(str.ToCharArray().Reverse().ToArray());
        public static bool IsEmptyOrWhiteSpace(this string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsEven(this int i) => i % 2 == 0;
        public static bool IsOdd(this int i) => i % 2 != 0;
    }
}

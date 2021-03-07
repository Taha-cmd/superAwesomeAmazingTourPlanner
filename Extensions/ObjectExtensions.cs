using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj) => obj == null;
    }
}

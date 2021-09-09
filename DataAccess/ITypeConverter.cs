using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace DataAccess
{
    public interface ITypeConverter
    {

        // this default implementation is working with postgres and sqlite
        // should different providers cause problems, we can implement a class implementing this interface and override the behavior
        public int ToInteger(object obj) => Convert.ToInt32(obj);
        public long ToLong(object obj) => Convert.ToInt64(obj);
        public string ToString(object obj) => Convert.ToString(obj);
        public DateTime ToDateTime(object obj) => Convert.ToDateTime(obj);
        public double ToDouble(object obj) => Convert.ToDouble(obj);
        public bool ToBoolean(object obj) => Convert.ToBoolean(obj);

        public T To<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}

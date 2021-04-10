using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class NpgsqlExtensions
    {
        public static T GetValue<T>(this NpgsqlDataReader reader, string key)
        {
            int index = reader.GetOrdinal(key);
            return reader.IsDBNull(index) ? default(T) : reader.GetFieldValue<T>(index);
        }
    }
}

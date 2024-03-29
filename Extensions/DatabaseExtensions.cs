﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Extensions
{
    public static class DatabaseExtensions
    {
        public static T GetValue<T>(this DbDataReader reader, string key)
        {
            int index = reader.GetOrdinal(key);
            return reader.IsDBNull(index) ? default : reader.GetFieldValue<T>(index);
        }

        public static object GetValue(this DbDataReader reader, string key)
        {
            int index = reader.GetOrdinal(key);
            return reader.IsDBNull(index) ? default : reader.GetValue(index);
        }



    }
}

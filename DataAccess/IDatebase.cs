﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataAccess
{
    // namespace System.Data.Common includes the abstract classes that all database providers implement (postgres, oracle, mysql ...)
    // this interface can be reused to create other database sources
    // a repository can rely on this interface
    public interface IDatebase
    {
        int ExecuteNonQuery(string statement, params DbParameter[] parameters);
        IEnumerable<TResult> ExecuteQuery<TResult>(string statement, Func<DbDataReader, TResult> rowReader, params DbParameter[] parameters);
        DbParameter Param<TValue>(string key, TValue value);
        public ITypeConverter TypeConverter { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;

namespace DataAccess
{
    public class SqliteDatabase : IDatebase
    {
        private readonly string connectionString;

        public ITypeConverter TypeConverter { get; } = new SqliteTypeConverter();

        public SqliteDatabase(string conn)
        {
            connectionString = conn;
        }

        public int ExecuteNonQuery(string statement, params DbParameter[] parameters)
        {
            using var conn = GetConnection();
            using var command = CreateCommand(statement, conn, parameters);

            return command.ExecuteNonQuery();
        }

        private IEnumerable<TResult> ReadRows<TResult>(SQLiteCommand command, Func<SQLiteDataReader, TResult> rowReader)
        {
            using var reader = command.ExecuteReader();
            var results = new List<TResult>();
            while (reader.Read())
                results.Add(rowReader(reader));

            return results;
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string statement, Func<DbDataReader, TResult> rowReader, params DbParameter[] parameters)
        {
            using var conn = GetConnection();
            using var command = CreateCommand(statement, conn, parameters);

            return ReadRows(command, rowReader);
        }

        public DbParameter Param<TValue>(string key, TValue value)
        {
            //no generic variant
            return new SQLiteParameter
            {
                ParameterName = key,
                Value = value
            };
        }

        private SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(connectionString);
            conn.Open();

            return conn;
        }

        private SQLiteCommand CreateCommand(string statement, SQLiteConnection conn, params DbParameter[] parameters)
        {
            var command = new SQLiteCommand(statement, conn);
            command.Parameters.AddRange(parameters);
            command.Prepare();

            return command;
        }
    }
}

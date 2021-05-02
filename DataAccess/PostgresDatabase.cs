using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccess
{
    public class PostgresDatabase : IDatebase
    {
        private string connectionString;
        public PostgresDatabase(string conn) => connectionString = conn;
        public int ExecuteNonQuery(string statement, params DbParameter[] parameters)
        {
            using var conn = GetConnection();
            using var command = CreateCommand(statement, conn, parameters);

            return command.ExecuteNonQuery();
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string statement, Func<DbDataReader, TResult> rowReader, params DbParameter[] parameters)
        {
            using var conn = GetConnection();
            using var command = CreateCommand(statement, conn, parameters);

            return ReadRows(command, rowReader);
        }

        private IEnumerable<TResult> ReadRows<TResult>(NpgsqlCommand command, Func<NpgsqlDataReader, TResult> rowReader)
        {
            using var reader = command.ExecuteReader();
            var results = new List<TResult>();
            while (reader.Read())
                results.Add(rowReader(reader));

            return results;
        }

        #region wrapper methods to make life easier
        public DbParameter Param<T>(string key, T value) => new NpgsqlParameter<T>(key, value);

        private NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            return conn;
        }
        private NpgsqlCommand CreateCommand(string statement, NpgsqlConnection conn, params DbParameter[] parameters)
        {
            var command = new NpgsqlCommand(statement, conn);
            command.Parameters.AddRange(parameters);
            command.Prepare();

            return command;
        }
        #endregion
    }
}

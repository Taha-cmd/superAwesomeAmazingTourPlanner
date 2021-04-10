using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using Extensions;

namespace DataAccess
{
    public class PostgresDatabase : Database
    {
        public PostgresDatabase(string conn)
        {
            connectionString = conn;
        }

        public int ExecuteNonQuery(string statement, params NpgsqlParameter[] parameters)
        {
            using var conn = GetConnection();
            using var command = Command(statement, conn, parameters);

            return command.ExecuteNonQuery();
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string statement, Func<NpgsqlDataReader, TResult> rowReader, params NpgsqlParameter[] parameters)
        {
            using var conn = GetConnection();
            using var command = Command(statement, conn, parameters);

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
        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            return conn;
        }
        public NpgsqlParameter Param<T>(string key, T value) => new NpgsqlParameter<T>(key, value);
        public NpgsqlCommand Command(string statement, NpgsqlConnection conn, params NpgsqlParameter[] parameters)
        {
            var command = new NpgsqlCommand(statement, conn);
            command.Parameters.AddRange(parameters);
            command.Prepare();

            return command;
        }
        #endregion


    }
}

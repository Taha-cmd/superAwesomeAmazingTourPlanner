using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class RepositoryBase
    {
        protected IDatebase database;
        public RepositoryBase(IDatebase database) => this.database = database;


        // generic method to retrieve one value
        // the method doesn't actually count the found values
        // if the filter returns more than one value, only the first one will be returned
        protected TValue GetValue<TValue, TFilter>(string table, string filter, TFilter filterValue, string columnToFetch, string filterOperator = "=")
        {
            string statement = $"SELECT {columnToFetch} FROM \"{table}\" WHERE {filter} {filterOperator} @filterValue";
            return database.ExecuteQuery(statement, (DbDataReader reader) => reader.GetFieldValue<TValue>(0), database.Param("filterValue", filterValue)).First();
        }

        protected int Count<TFilter>(string table, string filter, TFilter filterValue, string filterOperator = "=")
        {
            return GetValue<int, TFilter>(table, filter, filterValue, "COUNT(*)", filterOperator);
        }

        protected bool Exists<TFilter>(string table, string filter, TFilter filterValue)
        {
            return Count(table, filter, filterValue) == 1;
        }

        //for now this only works with one value
        //TODO: abstract it even more to update n Values (pass a list of tuples)
        protected int Update<TFilter, TValue>(string table, string filter, TFilter filterValue, string column, TValue newValue, string filterOperator = "=")
        {
            string statement = $"UPDATE \"{table}\" SET {column}=@newValue WHERE {filter} {filterOperator} @filterValue";
            return database.ExecuteNonQuery(statement, database.Param("newValue", newValue), database.Param("filterValue", filterValue));
        }
    }
}

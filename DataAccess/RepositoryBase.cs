using System.Data.Common;
using System.Linq;
using SqlKata;
using SqlKata.Compilers;

namespace DataAccess
{
    public class RepositoryBase
    {
        protected readonly IDatebase database;
        protected readonly Compiler queryCompiler;
        public RepositoryBase(IDatebase database, Compiler compiler)
        {
            this.database = database;
            this.queryCompiler = compiler;
        }


        // generic method to retrieve one value
        // the method doesn't actually count the found values
        // if the filter returns more than one value, only the first one will be returned
        protected TValue GetValue<TValue, TFilter>(string table, string filter, TFilter filterValue, string columnToFetch, string filterOperator = "=")
        {
            //select vs selectRaw: https://sqlkata.com/docs/select
            var query = new Query(table).SelectRaw(columnToFetch).Where(filter, filterOperator, filterValue);
            string statement = queryCompiler.Compile(query).Sql;
            return database.ExecuteQuery(statement, (DbDataReader reader) => reader.GetFieldValue<TValue>(0), database.Param("p0", filterValue)).First();
        }

        protected long Count<TFilter>(string table, string filter, TFilter filterValue, string filterOperator = "=")
        {
            return GetValue<int, TFilter>(table, filter, filterValue, "COUNT(*)", filterOperator);
            //
        }

        protected bool Exists<TFilter>(string table, string filter, TFilter filterValue)
        {
            return Count(table, filter, filterValue) >= 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class RepositoryBase
    {
        protected PostgresDatabase database;
        public RepositoryBase(string connectionString)
        {
            database = new PostgresDatabase(connectionString);
        }
    }
}

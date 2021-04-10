using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Extensions;

namespace BusinessLogic
{
    public static class Application
    {
        private static ToursManager manager;
        private const string configFilePath = "../../../../config.json";

        public static ToursManager GetToursManager()
        {
            if (manager.IsNull())
            {
                Config.Instance.LoadAndParseConfigFile(configFilePath);

                var database = new PostgresDatabase(Config.Instance.DataBaseConnectionString);
                var toursRepo = new ToursRepository(database);
                manager = new ToursManager(toursRepo);
            }
                

            return manager;
        }
    }
}

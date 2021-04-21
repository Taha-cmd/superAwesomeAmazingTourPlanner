using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Extensions;
using DataAccess.Maps;

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
                var mapsClient = new MapQuestClient(Config.Instance.MapsApiKey, Config.Instance.ImagesFolderPath);

                manager = new ToursManager(toursRepo, mapsClient);
            }
                

            return manager;
        }
    }
}

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
                manager = new ToursManager(new ToursRepository(Config.Instance.DataBaseConnectionString));
            }
                

            return manager;
        }
    }
}

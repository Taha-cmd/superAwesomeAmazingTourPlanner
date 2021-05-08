using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Extensions;
using DataAccess.Maps;
using log4net;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BusinessLogic
{
    public static class Application
    {
        private static ToursManager manager;

        // when in release mode, look for config file in the current directory when the binaries are
        
        #if DEBUG
            private const string configFilePath = "../../../../config.json";
#else
            private const string configFilePath = "config.json";
#endif

        public static ToursManager GetToursManager()
        {
            if (manager.IsNull())
            {
                Config.Instance.LoadAndParseConfigFile(configFilePath);


                // set the logging level based on the config file
                ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = LogManager.GetRepository().LevelMap[Config.Instance.LoggingLevel];
                ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);

                var database = new PostgresDatabase(Config.Instance.DataBaseConnectionString);
                var toursRepo = new ToursRepository(database);
                var mapsClient = new MapQuestClient(Config.Instance.MapsApiKey, Config.Instance.ImagesFolderPath);

                manager = new ToursManager(toursRepo, mapsClient);
            }
                

            return manager;
        }

        public static ILog GetLogger([CallerFilePath]string filename = "") => LogManager.GetLogger(filename);
    }
}

using DataAccess;
using DataAccess.Maps;
using Extensions;
using log4net;
using System;
using System.Runtime.CompilerServices;
using SqlKata.Compilers;
using System.Data.SQLite;
using Npgsql;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BusinessLogic
{
    public static class Application
    {
        private static ToursManager manager;

        // always copy config file to build dir
        private const string configFilePath = "config.json";

        public static ToursManager GetToursManager()
        {
            if (manager.IsNull())
            {
                Config.Instance.LoadAndParseConfigFile(configFilePath);


                // set the logging level based on the config file
                ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = LogManager.GetRepository().LevelMap[Config.Instance.LoggingLevel];
                ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);

                var database = new Database<SQLiteConnection, SQLiteCommand, SQLiteParameter, TypeConverterBase>(Config.Instance.DataBaseConnectionString);
                //var database = new Database<NpgsqlConnection, NpgsqlCommand, NpgsqlParameter, TypeConverterBase>(Config.Instance.DataBaseConnectionString);

                var toursRepo = new ToursRepository(database, new SqliteCompiler());
                var mapsClient = new MapQuestClient(Config.Instance.MapsApiKey, Config.Instance.ImagesFolderPath);
                var pdfGenerator = new PdfGenerator(Config.Instance.ReportsFolderPath);

                manager = new ToursManager(toursRepo, mapsClient, pdfGenerator);
            }
                

            return manager;
        }

        public static ILog GetLogger([CallerFilePath]string filename = "") => LogManager.GetLogger(filename);
    }
}

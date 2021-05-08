using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BusinessLogic
{
    public class Config // singleton
    {
        public static Config Instance { get; private set; }
        static Config() => Instance = new Config();


        public string DataBaseConnectionString { get; private set; }
        public string ImagesFolderPath { get; private set; }
        public string ExportsFolderPath { get; private set; }
        public string MapsApiKey { get; private set; }
        public string LoggingLevel { get; private set; }

        private Config() { }
        public void LoadAndParseConfigFile(string configFilePath)
        {
            if (!File.Exists(configFilePath))
                throw new Exception($"could not find config file at {configFilePath}");

            IConfiguration conf = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath(configFilePath)).Build();

            DataBaseConnectionString = $"Server={conf["Database:Server"]};" +
                $"Username={conf["Database:User"]};" +
                $"Database={conf["Database:Name"]};" +
                $"Port={conf["Database:Port"]};" +
                $"Password={conf["Database:Password"]};" +
                $"SSLMode=Prefer";

            ImagesFolderPath = conf["LocalStorage:Images"];
            ExportsFolderPath = conf["LocalStorage:Exports"];
            MapsApiKey = conf["Maps:Key"];
            LoggingLevel = conf["Logging:Level"];


#if !DEBUG
                ExportsFolderPath = Path.Join("LocalStorage", Path.GetFileName(ExportsFolderPath));
                ImagesFolderPath = Path.Join("LocalStorage", Path.GetFileName(ImagesFolderPath));
#endif

            if (!Directory.Exists(ImagesFolderPath) || !Directory.Exists(ExportsFolderPath))
                throw new Exception("path in config file does not exist " + ImagesFolderPath + " or " + ExportsFolderPath);
        }
    }
}

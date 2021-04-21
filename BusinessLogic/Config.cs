using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        private Config() { }
        public void LoadAndParseConfigFile(string configFilePath)
        {
            if (!File.Exists(configFilePath))
                throw new Exception($"could not find config file at {configFilePath}");

            string jsonString = File.ReadAllText(configFilePath);
            var configData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonString);
            var dbConf = configData["Database"];

            DataBaseConnectionString = $"Server={dbConf["Server"]};" +
                $"Username={dbConf["User"]};" +
                $"Database={dbConf["Name"]};" +
                $"Port={dbConf["Port"]};" +
                $"Password={dbConf["Password"]};" +
                $"SSLMode=Prefer";

            ImagesFolderPath = configData["LocalStorage"]["Images"];
            ExportsFolderPath = configData["LocalStorage"]["Exports"];
            MapsApiKey = configData["Maps"]["Key"];

            if (!Directory.Exists(ImagesFolderPath) || !Directory.Exists(ExportsFolderPath))
                throw new Exception("path in config file does not exist");
        }

        
    }
}

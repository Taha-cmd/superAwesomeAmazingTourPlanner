﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Diagnostics;

namespace BusinessLogic
{
    public class Config // singleton
    {
        public static Config Instance { get; private set; }
        static Config() => Instance = new Config();


        public string DataBaseConnectionString { get; private set; }
        public string ImagesFolderPath { get; private set; }
        public string ExportsFolderPath { get; private set; }
        public string ReportsFolderPath { get; private set; }
        public string MapsApiKey { get; private set; }
        public string LoggingLevel { get; private set; }
        public string LogoPath { get; private set; }
        private Config() { }
        public void LoadAndParseConfigFile(string configFilePath)
        {
            if (!File.Exists(configFilePath))
                throw new Exception($"could not find config file at {configFilePath}");

            IConfiguration conf = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath(configFilePath)).Build();

            DataBaseConnectionString = conf["Database:ConnectionString"];
            ImagesFolderPath = conf["LocalStorage:Images"];
            ExportsFolderPath = conf["LocalStorage:Exports"];
            ReportsFolderPath = conf["LocalStorage:Reports"];
            MapsApiKey = conf["Maps:Key"];
            LoggingLevel = conf["Logging:Level"];
            LogoPath = conf["Assets:Logo"];


            if (!Directory.Exists(ImagesFolderPath) || !Directory.Exists(ExportsFolderPath) || !Directory.Exists(ReportsFolderPath) || !File.Exists(LogoPath))
                throw new Exception("path in config file does not exist " + ImagesFolderPath + " or " + ExportsFolderPath + " or " + ReportsFolderPath + " or " + LogoPath);
        }
    }
}

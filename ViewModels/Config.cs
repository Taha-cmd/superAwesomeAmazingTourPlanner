using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class Config // singleton
    {
        public static Config Instance { get; private set; }
        static Config() => Instance = new Config();


        public string DataBaseConnectionString { get; private set; }
        private Config()
        {
            var builder = new StringBuilder();

            builder.Append($"Server={UserEnvVar("TourPlanner_DB_Host")};");
            builder.Append($"Username={UserEnvVar("TourPlanner_DB_User")};");
            builder.Append($"Database={UserEnvVar("TourPlanner_DB_Name")};");
            builder.Append($"Port={UserEnvVar("TourPlanner_DB_Port")};");
            builder.Append($"Password={UserEnvVar("TourPlanner_DB_Password")};");
            builder.Append($"SSLMode=Prefer;");

            DataBaseConnectionString = builder.ToString();
        }

        #region enviroment variables
        // the following enviroment variables need to be set on a user level

        // TourPlanner_DB_Host
        // TourPlanner_DB_Port
        // TourPlanner_DB_Name
        // TourPlanner_DB_User
        // TourPlanner_DB_Password
        #endregion

        private string UserEnvVar(string varName) => Environment.GetEnvironmentVariable(varName, EnvironmentVariableTarget.User);
        
    }
}

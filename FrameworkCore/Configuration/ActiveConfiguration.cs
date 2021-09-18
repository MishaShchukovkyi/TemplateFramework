using System;
using System.Configuration;

namespace FrameworkCore.Configuration
{
    public class ActiveConfiguration
    {
        public static string AppUrl => GetFromEnviormentVariableSettings("Application_URL");

        public static string bsUsername => GetFromEnviormentVariableSettings("browserStackUsername");
        public static string bsKey => GetFromEnviormentVariableSettings("browserStackKey");
        public static string bsServer => GetFromEnviormentVariableSettings("browserStackServer");
        public static string bsProject => GetFromEnviormentVariableSettings("browserStackProject");


        private static string GetFromEnviormentVariableSettings(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? ConfigurationSettings.AppSettings[key];
        }
    }
}

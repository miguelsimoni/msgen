using System;
using System.Configuration;

namespace msgen.config
{
    public static class Configurator
    {
        public static void saveConfig(string key, string value)
        {
            Configuration confg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            confg.AppSettings.Settings[key].Value = value;
            confg.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("AppSettings");
        }

        public static string configValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}

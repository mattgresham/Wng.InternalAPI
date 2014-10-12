using System;
using System.Configuration;

namespace Wng.InternalApi.Helpers
{
    /// <summary>
    /// Provide a Helper Methods for retrieving config data
    /// </summary>
    public static class ParameterHelperMethods
    {
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static string GetStringParamFromAppSettings(string key, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value) == false)
            {
                return value;
            }

            return defaultValue;
        }

        public static string[] GetMultipleStringParamsFromAppSettings(string key, string defaultValue = "", char delimiter = ';')
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value) == false)
            {
                return value.Split(new [] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new [] { defaultValue };
        }

        public static bool GetBoolParamFromAppSettings(string key, bool defaultValue = false)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (value != null)
            {
                if (value.ToUpper() != "TRUE" && value.ToUpper() != "FALSE")
                {
                    throw new ApplicationException("Invalid App.config entry for: " + key);
                }

                return (value.ToUpper() == "TRUE");
            }

            return defaultValue;
        }

        public static int GetIntegerParamFromAppSettings(string key, int defaultValue = 0)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value) == false)
            {
                return int.Parse(value);
            }

            return defaultValue;
        }
    }
}

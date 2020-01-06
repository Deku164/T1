using System;
using System.Configuration;
using System.Linq;

namespace SocketServer.Class
{
    public static class clsConfig
    {
        /// <summary>
        /// App.config에서 키에 대한 값을 읽어온다.
        /// </summary>
        /// <param name="keyName">Key</param>
        /// <returns>Value</returns>
        public static string ReadAppConfig(string keyName)
        {
            string data = "";
            try
            {
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (currentConfig.AppSettings.Settings.AllKeys.Contains(keyName)) data = currentConfig.AppSettings.Settings[keyName].Value.Trim();
                else data = ""; //키가없으면.
            }
            catch { throw new Exception(); }
            return data;
        }


        /// <summary>
        /// App.config에 키에 대한 값을 입력한다.
        /// </summary>
        /// <param name="keyName">Key</param>
        /// <param name="value">Key</param>
        /// <returns>True/False</returns>
        public static bool WriteAppConfig(string keyName, string value)
        {
            Boolean ret = false;
            try
            {
                value = value.Trim();
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (currentConfig.AppSettings.Settings.AllKeys.Contains(keyName)) currentConfig.AppSettings.Settings[keyName].Value = value;
                else currentConfig.AppSettings.Settings.Add(keyName, value);

                currentConfig.Save();

                ConfigurationManager.RefreshSection("appSettings");  // 내용 갱신     

                ret = true;
            }
            catch { throw new Exception(); }
            return ret;
        }
    }
}

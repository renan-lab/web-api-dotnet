using System;
using System.Configuration;
using System.IO;

namespace web_api.Configurations
{
    public class Log
    {
        public static string getFullPath()
        {
            string fileName = $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
            string path = ConfigurationManager.AppSettings["logPath"];
            string fullPath = Path.Combine(path, fileName);
            return fullPath;
        }
    }
}
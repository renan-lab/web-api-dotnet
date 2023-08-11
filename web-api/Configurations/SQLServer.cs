using System;
using System.Configuration;

namespace web_api.Configurations
{
    public class SQLServer
    {
        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["consultorio"].ConnectionString;
        }
    }
}
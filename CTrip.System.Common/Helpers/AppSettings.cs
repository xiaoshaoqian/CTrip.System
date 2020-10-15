using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTrip.System.Common.Helpers
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
        static AppSettings()
        {
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                .Build();
        }
        public static string GetJson(string jsonPath, string key)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile(jsonPath).Build();
            string s = config.GetSection(key).Value;
            return s;
        }
    }
}

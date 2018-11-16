
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Configuration;
using System.IO;

namespace DotNet.Common
{
   public static  class ConfigHelper
    {
        public static readonly IConfiguration Configuration;

        static ConfigHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();
        }

        public static T GetSection<T>(string key) where T : class, new()
        {
            Configuration.GetSection(key);
            var obj = new ServiceCollection()
                .AddOptions()
                .Configure<T>(Configuration.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return obj;
        }

        public static string GetSection(string key)
        {
            return Configuration.GetValue<string>(key);
        }
    }
}

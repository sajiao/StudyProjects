
using System;
using System.Configuration;

namespace DotNet.Common
{
   public static  class ConfigHelper
    {
       public static string GetConfigValue(string name)
       {
           try
           {
               return ConfigurationManager.AppSettings[name];
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Common
{
    using System.Runtime.Caching;
    using System.Web.Caching;

    using CacheItemPriority = System.Web.Caching.CacheItemPriority;

    public static class CacheHelper
    {

        public static MemoryCache Caches = new MemoryCache("CommonMemoryCache");

       private static string keySplit = ":";

       public static bool Exists(string key)
       {
           return Caches.Contains(key);
       }

       public static string CreateKey<T>(string idFieldValue)
       {
           return CreateKey(typeof(T), idFieldValue);
       }

       public static string CreateKey(System.Type objectType, string idFieldValue)
       {
           if (idFieldValue.Contains(keySplit))
           {
               throw new System.ArgumentException("idFieldValue cannot have the illegal characters: ':'", "idFieldValue");
           }
           return string.Format("urn:{0}:{1}", objectType.Name, idFieldValue);
       }
      
       public static string CreateKey<T>(string idFieldName, string idFieldValue)
       {
           return CreateKey(typeof(T), idFieldName, idFieldValue);
       }

       public static string CreateKey(System.Type objectType, string idFieldName, string idFieldValue)
       {
           if (idFieldValue.Contains(keySplit))
           {
               throw new System.ArgumentException("idFieldValue cannot have the illegal characters: ':'", "idFieldValue");
           }
           if (idFieldName.Contains(keySplit))
           {
               throw new System.ArgumentException("idFieldName cannot have the illegal characters: ':'", "idFieldName");
           }

           return string.Format("urn:{0}:{1}:{2}", objectType.Name, idFieldName, idFieldValue);
       }

        public static void Add(string key,object obj, DateTime dt)
        {
            if (Caches.Contains(key) == false && obj != null) Caches.Set(key, obj, new DateTimeOffset(dt));
        }

        public static T Get<T>(string key) where T : class 
        {
            if (Caches.Contains(key)) return Caches.Get(key) as T;

            return null;
        }
    }
}

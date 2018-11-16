using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
{
   public static class ListExtensions
    {
        /// <summary>
        /// default delimiter ','
        /// </summary>
        /// <param name="source"></param>
        /// <param name="delimiter">default ','</param>
        /// <returns></returns>
        public static string ToListString(this List<string> source, char delimiter = ',')
        {
            string restult = string.Empty;
            if (source == null) return restult;
           
            source.ForEach(s => restult += s + delimiter);

            return restult.TrimEnd(delimiter);
        }

        public static bool HasValue<T>(this List<T> source)
        {
            return source != null && source.Count > 0;
        }

        public static bool IsNullOrEmpty<T>(this List<T> source)
        {
            return source == null || source.Count == 0;
        }

        public static string StringConvertToStrings(this List<String> source, string format)
        {
            if (source != null && source.Count > 0)
            {
                return source.Aggregate(string.Empty, (current, id) => current + string.Format(format, id));
            }
            else
            {
                return string.Empty;
            }
        }


    }
}

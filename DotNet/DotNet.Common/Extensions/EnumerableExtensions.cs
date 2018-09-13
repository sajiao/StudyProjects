using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common.Extensions
{
   public static class EnumerableExtensions
    {
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="source">拼接为字符串</param>
        /// <returns></returns>
        public static String JoinToString<T>(this IEnumerable<T> source, string split = ",")
        {
            if (source == null || source.Count() == 0)
                return String.Empty;

            return String.Join(split, source);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
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

        /// <summary>
        /// 集合对象是否为Null或者没有项
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns>没有值或者空返回true，有值返回false</returns>
        public static bool IsNullOrEmpty(this IEnumerable<object> thisValue)
        {
            if (thisValue == null || thisValue.Count() == 0) return true;
            return false;
        }

        /// <summary>
        /// 集合对象是否有值判断
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns>有值返回true,没有值或者空返回false</returns>
        public static bool HasValue(this IEnumerable<object> thisValue)
        {
            if (thisValue == null || thisValue.Count() == 0) return false;
            return true;
        }

    }
}

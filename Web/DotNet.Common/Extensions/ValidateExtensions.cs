using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Common.Extensions
{
   public static  class ValidateExtensions
    {
        /// <summary>
        /// 是否在输入数值之间(包含开始结束)
        /// </summary>
        /// <param name="thisValue">待比较值</param>
        /// <param name="begin">开始</param>
        /// <param name="end">结束</param>
        /// <returns>包含返回true,不包含返回false</returns>
        public static bool IsInRange(this int thisValue, int begin, int end)
        {
            return thisValue >= begin && thisValue <= end;
        }

        /// <summary>
        /// 是否在输入参数中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="thisValue">当前值</param>
        /// <param name="values">输入参数</param>
        /// <returns>包含返回true,不包含返回false</returns>
        public static bool IsIn<T>(this T thisValue, params T[] values)
        {
            return values.Contains(thisValue);
        }

        /// <summary>
        /// GUID?是否为Null或者空
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns>为Null或者空返回true,否则返回false</returns>
        public static bool IsNullOrEmpty(this Guid? thisValue)
        {
            if (thisValue == null) return true;
            return thisValue == Guid.Empty;
        }

        /// <summary>
        /// GUID是否为Null或者空
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns>为Null或者空返回true,否则返回false</returns>
        public static bool IsNullOrEmpty(this Guid thisValue)
        {
            if (thisValue == null) return true;
            return thisValue == Guid.Empty;
        }

      
    }
}

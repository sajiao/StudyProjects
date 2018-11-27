using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNet.Common
{
   public static class StringExtenstions
    {
        /// <summary>
        /// 当前值是否包含输入参数
        /// </summary>
        /// <param name="thisValue">当前值</param>
        /// <param name="inValues">输入参数</param>
        /// <returns>任意包含返回true,不包含返回false</returns>
        public static bool IsContainsIn(this string thisValue, params string[] inValues)
        {
            return inValues.Any(it => thisValue.Contains(it));
        }

        public static bool IsEqual(this string thisValue, params string[] inValues)
        {
            var result =  inValues.Any(it => thisValue.EqualsCurrentCultureIgnoreCase(it.TryTrim()));
            return result;
        }

        /// <summary>
        /// 追加字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format">分割符号</param>
        /// <returns></returns>
        public static String Append(this String s, string str, char format = ',')
        {
            try
            {
                s += format.ToString() + str;
                return s.Trim(format);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "  s:" + s + "; str:" + str, ex);
            }
        }

        public static bool IsNumerics(this string source)
        {
            if (!string.IsNullOrEmpty(source) && Regex.IsMatch(source, @"^[\+\-]?\d*([\.]\d+)?$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsURI(this string source)
        {
            string url = @"^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$";
            return Regex.IsMatch(source, url);
        }

        public static string EncodeHtml(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                source = source.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("  ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#8217;");
            }

            return source;
        }

        public static string DecodeHtml(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                source = source.Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&nbsp;", " ").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&#8217;", "'");
            }

            return source;
        }

        public static string TryTrim(this string source)
        {
            return source == null ? source : source.Trim();
        }

        public static string TryTrimEnd(this string source)
        {
            return source == null ? source : source.TrimEnd();
        }

        public static string TryTrimStart(this string source)
        {
            return source == null ? source : source.TrimStart();
        }

        public static int TryToInt(this string str)
        {
            int result;
            int.TryParse(str, out result);
            return result;
        }

        public static Decimal TryToDecimal(this string str)
        {
            Decimal result;
            Decimal.TryParse(str, out result);
            return result;
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        public static bool EqualsOrdinalIgnoreCase(this string source, string obj)
        {
            return source == null ? source == obj : source.Equals(obj, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EqualsCurrentCultureIgnoreCase(this string source, string obj)
        {
            return source == null ? source == obj : source.Equals(obj, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string Format(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        public static string Format(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        public static string Format(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        public static DateTime? ToNullableDateTime(this string source)
        {
            if (source.IsNullOrEmpty()) return null;
            DateTime result;
            if (DateTime.TryParse(source, out result)) return result;
            else return null;

        }

        public static DateTime TryToDateTime(this string source)
        {
           
            DateTime result;
            if (DateTime.TryParse(source, out result)) return result;

            return result;
        }

        public static DateTime TryToDateTimeByFormat(this string source, string format ="yyyyMMdd")
        {
            DateTime result;
            if (DateTime.TryParseExact(source, format, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out result)) return result;
            return result;
        }

        public static DateTime TryToDateTime(this string str, DateTime defaultValue)
        {
            DateTime result;
            if (DateTime.TryParse(str, out result) == false)
                result = defaultValue;
            return result;
        }

        public static string TryToDateTimeDayStart(this string str)
        {
            string result = string.Empty;
            var datetime = TryToDateTime(str, DateTime.MinValue);
            if (datetime != DateTime.MinValue)
            {
                result = datetime.ToString("yyyy-MM-dd");
            }
            return result;
        }

        public static string TryToDateTimeDayEnd(this string str)
        {
            string result = string.Empty;
            var datetime = TryToDateTime(str, DateTime.MinValue);
            if (datetime != DateTime.MinValue)
            {
                result = datetime.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            return result;
        }

        public static string FormatDateTime(this string source, string format)
        {
            DateTime? result = source.ToNullableDateTime();
            if (result != null) result.Value.ToString(format);
            return string.Empty;
        }


        /// <summary>
        /// 将字符串分割为数字集合,默认是","
        /// </summary>
        /// <param name="s">要分割的字符串,格式为：value1,value2</param>
        /// <returns>分割之后的数字集合</returns>
        public static List<Int32> SplitToList(this String s, char split = ',')
        {
            try
            {
                if (String.IsNullOrWhiteSpace(s) == false)
                {
                    String[] nums = s.Split(split);
                   var  tempList = new List<Int32>(nums.Length);
                    foreach (var item in nums)
                    {
                        if (String.IsNullOrWhiteSpace(item) == false)
                            tempList.Add(Convert.ToInt32(item));
                    }
                    return tempList;
                }

                return new List<Int32>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "  s:" + s, ex);
            }
        }

    }
}

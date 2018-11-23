using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common.Extensions
{
  public static  class DateTimeExtensions
    {
        /// <summary>
        /// 是否在输入时间之间(包含开始结束)
        /// </summary>
        /// <param name="thisValue">待比较时间</param>
        /// <param name="begin">开始</param>
        /// <param name="end">结束</param>
        /// <returns>包含返回true,不包含返回false</returns>
        public static bool IsInRange(this DateTime thisValue, DateTime begin, DateTime end)
        {
            return thisValue >= begin && thisValue <= end;
        }

        public static string ToFormatString(this DateTime source)
        {
            if (source == DateTime.MinValue) return string.Empty;
            return source.ToString("dd/MM/yyyy");
        }

        public static string ToFormatTimeString(this DateTime source)
        {
            if (source == DateTime.MinValue) return string.Empty;
            return source.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToFormatString(this DateTime source, string format)
        {
            if (source == null) return string.Empty;
            return source.ToString(format);
        }

        public static string DateDiff(this DateTime dt1, DateTime dt2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(dt1.Ticks);
            TimeSpan ts2 = new TimeSpan(dt2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + " day " +
                ts.Hours.ToString() + " hour " +
                ts.Minutes.ToString() + " m " +
                ts.Seconds.ToString() + " s ";
            return dateDiff;
        }

        public static DateTime FirstDayOfMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        public static DateTime LastDayOfMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfPreviousMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(-1);
        }

        public static DateTime LastDayOfPrdviousMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddDays(-1);
        }

        public static string GetDayOfWeek(this DateTime datetime, bool isChinese = false)
        {
            return GetDayOfWeek(datetime.DayOfWeek, isChinese);
        }

        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime dt)
        {
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        #region 根据某年的第几周获取这周的起止日期
        /// <summary>
        /// 根据某年的第几周获取这周的起止日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOrder"></param>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public static void WeekRange(this DateTime dt, int weekOrder, ref DateTime firstDate, ref DateTime lastDate)
        {
            //当年的第一天
            var firstDay = new DateTime(dt.Year, 1, 1);

            //当年的第一天是星期几
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);

            //计算当年第一周的起止日期，可能跨年
            int dayDiff = (-1) * firstOfWeek + 1;
            int dayAdd = 7 - firstOfWeek;

            firstDate = firstDay.AddDays(dayDiff).Date;
            lastDate = firstDay.AddDays(dayAdd).Date;

            //如果不是要求计算第一周
            if (weekOrder != 1)
            {
                int addDays = (weekOrder - 1) * 7;
                firstDate = firstDate.AddDays(addDays);
                lastDate = lastDate.AddDays(addDays);
            }
        }
        #endregion

        #region 返回两个日期之间相差的天数
        /// <summary>
        /// 返回两个日期之间相差的天数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>天数</returns>
        public static int DiffDays(this DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto.Date - dtfrm.Date;
            return tsDiffer.Days;
        }
        #endregion

        #region 判断当前年份是否是闰年
        /// <summary>判断当前年份是否是闰年，私有函数</summary>
        /// <param name="iYear">年份</param>
        /// <returns>是闰年：True ，不是闰年：False</returns>
        private static bool IsRuYear(this DateTime dt)
        {
            //形式参数为年份
            //例如：2003
            int n = dt.Year;
            return (n % 400 == 0) || (n % 4 == 0 && n % 100 != 0);
        }
        #endregion

        private static string GetDayOfWeek(DayOfWeek dayOfWeek, bool isChinese)
        {
            string week = "0";
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    week = "0";
                    break;
                case DayOfWeek.Monday:
                    week = "1";
                    break;
                case DayOfWeek.Tuesday:
                    week = "2";
                    break;
                case DayOfWeek.Wednesday:
                    week = "3";
                    break;
                case DayOfWeek.Thursday:
                    week = "4";
                    break;
                case DayOfWeek.Friday:
                    week = "5";
                    break;
                case DayOfWeek.Saturday:
                    week = "6";
                    break;
                default:
                    break;
            }
            if (isChinese)
            {
                switch (week)
                {
                    case "0":
                        week = "星期日";
                        break;
                    case "1":
                        week = "星期一";
                        break;
                    case "2":
                        week = "星期二";
                        break;
                    case "3":
                        week = "星期三";
                        break;
                    case "4":
                        week = "星期四";
                        break;
                    case "5":
                        week = "星期五";
                        break;
                    case "6":
                        week = "星期六";
                        break;
                    default:
                        break;
                }
            }
            return week;
        }
    }

    public static class DateTimeNullExtensions
    {
        public static string ToFormatString(this DateTime? source)
        {
            return source.ToFormatString("dd/MM/yyyy");
        }

        public static string ToFormatString(this DateTime? source, string format)
        {
            if (source == null || source.HasValue == false) return string.Empty;
            return source.Value.ToString(format);
        }

        public static string ToFormatTimeString(this DateTime? source)
        {
            return source.ToFormatString("yyyy-MM-dd HH:mm:ss");
        }

        public static string TryToSQLStart(this DateTime? source)
        {
            string result = string.Empty;
            if (source != null && source.HasValue)
                result = source.ToFormatString("yyyy-MM-dd");
            return result;
        }

        public static string TryToSQLEnd(this DateTime? source)
        {
            string result = string.Empty;
            if (source != null && source.HasValue)
                result = source.ToFormatString("yyyy-MM-dd") + " 23:59:59";
            return result;
        }
    }

}

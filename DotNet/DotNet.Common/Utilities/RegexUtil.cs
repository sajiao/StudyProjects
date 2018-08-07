using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
{
   public  class RegexUtil
    {
        public static string PositiveInteger = @"^[0-9]*[1-9][0-9]*$";// 正整数正则表达式
        public static string Integer = @"^-?\d+$";// 整数正则表达式
        public static string Float = @"^[+|-]?\d*\.?\d*$";// 浮点数正则表达式
    }
}

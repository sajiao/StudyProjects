using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DotNet.Common
{
   public static class StringHelper
    {
        // Supported algorithms
        /// <summary>
        /// SHA1 160位(20字节)算法。
        /// </summary>
        //SHA1,

        /// <summary>
        /// SHA256 256位(32字节)算法。
        /// </summary>
        //SHA256, 

        /// <summary>
        /// SHA284 384位(48字节)算法。
        /// </summary>
        //SHA384, 

        /// <summary>
        /// SHA512 512位(64字节)算法。
        /// </summary>
        //SHA512, 

        /// <summary>
        /// MD5 128位(16字节)算法。
        /// </summary>
        //MD5
        /// <summary>
        ///根据传入的PWD字符串，得到相应MD5加密字符串的静态方法
        ///返回：md5加密字符串
        /// </summary>
        public static string MD5(string PWD)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(PWD, "MD5");
        }

        /// <summary>
        ///根据传入的PWD字符串，得到相应SHA1加密字符串的静态方法
        ///返回：SHA1加密字符串
        /// </summary>
        public static string SHA1(string PWD)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(PWD, "SHA1");
        }

        /// <summary>
        /// 产生指定长度的随机字符串
        /// </summary>
        public static string Random(int charLength)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] chars = new char[charLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < charLength; i++)
            {
                chars[i] = _allowedChars[(int)((allowedCharCount) * randNum.NextDouble())];
            }

            return new string(chars);
        }
        /// <summary>
        /// 产生指定长度的随机字符串
        /// </summary>
        public static string RandomNum(int charLength)
        {
            string _allowedChars = "0123456789";
            Random randNum = new Random();
            char[] chars = new char[charLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < charLength; i++)
            {
                chars[i] = _allowedChars[(int)((allowedCharCount) * randNum.NextDouble())];
            }

            return new string(chars);
        }
    }
}

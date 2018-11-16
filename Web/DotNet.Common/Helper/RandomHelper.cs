using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
{
   public class RandomHelper
    {
        public static int Minimum = 100000;
        public static int Maximal = 999999;
        public static int RandomLength = 6;

        private static string RandomString = "123456789ABCDEFGHIJKMLNPQRSTUVWXYZ";
        private static Random Random = new Random(DateTime.Now.Second);

        #region
        /// <summary>
        /// 产生随机字符
        /// </summary>
        /// <returns>字符串</returns>
        public static string GetRandomString()
        {
            string returnValue = string.Empty;
            for (int i = 0; i < RandomLength; i++)
            {
                int r = Random.Next(0, RandomString.Length - 1);
                returnValue += RandomString[r];
            }
            return returnValue;
        }
        #endregion

        #region
        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <returns>随机数</returns>
        public static int GetRandom()
        {
            return Random.Next(Minimum, Maximal);
        }
        #endregion

        #region
        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <param name="minimum">最小值</param>
        /// <param name="maximal">最大值</param>
        /// <returns>随机数</returns>
        public static int GetRandom(int minimum, int maximal)
        {
            return Random.Next(minimum, maximal);
        }
        #endregion
    }
}

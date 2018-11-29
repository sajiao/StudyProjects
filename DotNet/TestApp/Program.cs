using DotNet.Common;
using Fleck;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    

    class Program
    {
        static void Main(string[] args)
        {
            //var pBuf = new ProtocolBuffer();
            //pBuf.Test1();
            //Console.ReadLine();

            //string str = "1123||23423||||&&&&&&";
            //Console.WriteLine(str.TrimEnd('&').TrimEnd('|'));

            //Console.WriteLine(nameof(str));
            //(var a, var b, var c) = GetTest();
            //Console.WriteLine(a +" " + b + " "+ c);
            //setData();
            //foreach (var item in mData)
            //{
            //    Console.WriteLine(item.Num + " score: " + item.RankScore);
            //}

            //string str = "1,2,3";
            //str = str.Append("4");
            List<decimal> num = new List<decimal> { 1,23,4,7,56,7,8,90.0m,5,3,3,4,5.0m};

            SortTest(num, 44,3);

           // Console.WriteLine(str);
            Console.ReadLine();

        }

        public static void SortTest(List<decimal> args, decimal inputAmount, decimal floatAmount)
        {

            Dictionary<decimal, List<decimal>> result = new Dictionary<decimal, List<decimal>>();
            List<decimal> List2 = new List<decimal>(args);
            for (int i = 0; i < List2.Count; i++)
            {
                List<decimal> List3 = new List<decimal>();
                List3.Add(args[i]);
                List2.RemoveAt(i);
                for (int j = 0; j < List2.Count; j++)
                {
                    if (List2[j] <= inputAmount - Count(List3))
                    {
                        List3.Add(List2[j]);
                        List2.RemoveAt(j);
                        j = -1;
                    }
                }
                result[Count(List3)] = List3;
                i = -1;
            }
            var tempResult = result.Where(r => (inputAmount - r.Key) <= floatAmount).OrderByDescending(r => r.Key);
            if (tempResult.Count() == 0)
            {
                tempResult = result.OrderByDescending(r => inputAmount - r.Key);
            }
            var  t = tempResult.First();
            foreach (var item in tempResult)
            {
                string strArray = string.Join(",", item.Value);
                Console.WriteLine("Total:"+ item.Key +" ；"+ strArray);
            }
        }

        public static decimal Count(List<decimal> list)
        {
            return list.Sum(a => a);
        }

        private static void setData()
        {
            mData = new List<RebelRank>();
            mData.Add(new RebelRank() { Num = 1, RankScore = 200 });
            mData.Add(new RebelRank() { Num = 2, RankScore = 199 });
            mData.Add(new RebelRank() { Num = 3, RankScore = 100 });
            mData.Add(new RebelRank() { Num = 4, RankScore = 100 });
            mData.Add(new RebelRank() { Num = 5, RankScore = 100 });
            mData.Add(new RebelRank() { Num = 6, RankScore = 50 });
            mData.Add(new RebelRank() { Num = 7, RankScore = 30 });
            mData.Add(new RebelRank() { Num = 8, RankScore = 10 });
            AddRank(new RebelRank() { Num = 0, RankScore = 10 });
        }

        private static List<RebelRank> mData = null;

        private static void AddRank(RebelRank req)
        {
            var temp = mData.Where(m => m.RankScore > req.RankScore);
            int index = 0;
            if (temp == null)
            {
                req.Num = 1;
            }
            else
            {
                index = temp.Max(m => m.Num);
                req.Num = mData[index - 1].Num + 1; 
            }

            mData.Insert(index, req);
            for (int i = index + 1; i < mData.Count; i++)
            {
                mData[i].Num += 1;
            }
        }


        public static (int, int, string) GetTest() {
            return (1, 2, "3");
        }
    }

    public class RebelRank
    {
        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 玩家名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 玩家所在国家ID
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// 玩家头像ID
        /// </summary>
        public int HeadImageID { get; set; }

        /// <summary>
        /// 玩家分数
        /// </summary>
        public int RankScore { get; set; }

        /// <summary>
        /// 玩家排名
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 玩家分数更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}

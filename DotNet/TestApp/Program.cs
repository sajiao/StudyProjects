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
            setData();
            foreach (var item in mData)
            {
                Console.WriteLine(item.Num + " score: " + item.RankScore);
            }

            
            Console.ReadLine();

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

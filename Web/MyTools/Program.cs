using BLL;
using BLL.ThirtyPart;
using DotNet.Common;
using Entities;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MyTools
{
    public class WordBook
    {
        public List<Words> Item { get;set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //DAL.DB.GetDB();

            //var files = Directory.GetFiles("D:\\tb\\", "*.xls");

            //foreach (var item in files)
            //{
            //    if (item.Contains("done"))
            //    {
            //        continue;
            //    }

            //    if (item.Contains("聚划算拼团单品"))
            //    {
            //  TaoBaoImport.ImportJuTuan(item);
            //    }
            //    else if (item.Contains("精选优质商品清单"))
            //    {
            //         TaoBaoImport.ImportItems(item);
            //    }

            //}

            // var temp =  ObjectXmlSerializer.LoadFromXml<WordBook>(path);
            //var xmlPaths = "D:\\BaiduNetdiskDownload\\COCAXML";
            //WordImport.Import(xmlPaths);
            ImportTaobaoke();
            while (true)
            {
                var now = DateTime.Now;
                if (now.Hour / 8 == 0)
                {
                    ImportTaobaoke();
                }
                Thread.Sleep(1000 * 60 * 60);
            }
        
            //ImportItemDetail();
            //Console.ReadLine();
        }

        public static void ImportItemDetail()
        {
            //var items = ItemsBLL.GetData();
           
           // foreach (var item in items)
            //{
                var body = WebRequestHelper.GetResponseContent("https://h5.m.taobao.com/awp/core/detail.htm?id=588292874603&pid=mm_25162659_311500292_108899300176");
                //if (body.Contains("descUrl"))
                //{
                //    var temp = body.Remove(0, body.IndexOf("descUrl"));
                //    temp = temp.Remove(0, temp.IndexOf("?"));
                //    temp = temp.Substring(1, temp.IndexOf(":") -1).Replace("'", "").Trim();
                //    if (temp.StartsWith("http") == false)
                //    {
                //         temp = "http:" + temp;
                //    }
                //    item.ProductDetailUrl = temp;
                //}
                if (body.Contains("detail-content"))
                {
                   var temp = body.Remove(0, body.IndexOf("detail-content"));
                }
            //}


        }

        public static void ImportTaobaoke()
        {
            PageInfo page = new PageInfo();
            page.PageSize = 100;
            var cateItems = ItemCateBLL.GetData();
            var items = ItemsBLL.GetData();
            List<Items> updateItems = new List<Items>(500);
            List<Items> addItems = new List<Items>(500);
            foreach (var item in cateItems)
            {
                page.PageIndex = 1;
                var result = TaoBaoKe.QueryDgItemCoupon(page, 0, item.CateName,1);
                var tianmaoresult = TaoBaoKe.QueryDgItemCoupon(page, 0, item.CateName, 2);
                if (result.Count == 0 && tianmaoresult.Count == 0)
                {
                    return;
                }
                result.AddRange(tianmaoresult);
                TaoBaoKe.QueryProductDetail(result, 1);
                TaoBaoKe.QueryProductDetail(result, 2);
                foreach (var item2 in result)
                {
                    var temp = items.FirstOrDefault(a => a.NumIid == item2.NumIid);
                    if (temp != null)
                    {
                        item2.Id = temp.Id;
                        temp = item2;
                        if (updateItems.Exists(d => d.NumIid == temp.NumIid) == false)
                        {
                            updateItems.Add(temp);
                        }
                    }
                    else
                    {
                        items.Add(item2);
                        addItems.Add(item2);
                    }
                }
                
                if (addItems.Count >= 500)
                {
                    ItemsBLL.BatchInsert(addItems);
                    addItems.Clear();
                }
                if (updateItems.Count >= 500)
                {
                    ItemsBLL.BatchUpdate(updateItems);
                    updateItems.Clear();
                }
          }
 
            if (addItems.Count > 0)
            {
                ItemsBLL.BatchInsert(addItems);
                addItems.Clear();
            }

            if (updateItems.Count > 0)
            {
                ItemsBLL.BatchUpdate(updateItems);
                updateItems.Clear();
            }

            ItemsBLL.ClearSameData();

            Console.WriteLine("Done:"+ DateTime.Now);

            WebRequestHelper.GetResponseContent("http://api.bestddd.com/api/items/refresh/AutoReInit" + DateTime.Now.Hour.ToString());

            //TaskParallelHelper.ExecuteTask(actionItem);
        }

        public static void InitCate()
        {
            List<ItemCate> cates = new List<ItemCate>()
            {
                new ItemCate(){ CateId = 16, CateName ="女装"},
                new ItemCate(){ CateId = 50344007, CateName ="男装"},
                new ItemCate(){ CateId = 1625, CateName ="内衣"},
                new ItemCate(){ CateId = 50016853, CateName ="鞋靴"},
                new ItemCate(){ CateId = 50006842, CateName ="箱包"},
                new ItemCate(){ CateId = 50010404, CateName ="配件"},
                new ItemCate(){ CateId = 0, CateName ="童装玩具"},
                new ItemCate(){ CateId = 0, CateName ="孕产"},
                new ItemCate(){ CateId = 0, CateName ="用品"},
                new ItemCate(){ CateId = 0, CateName ="家电"},
                new ItemCate(){ CateId = 0, CateName ="数码"},
                new ItemCate(){ CateId = 0, CateName ="手机"},
                new ItemCate(){ CateId = 0, CateName ="美妆"},
                new ItemCate(){ CateId = 1801, CateName ="洗护"},
                new ItemCate(){ CateId = 0, CateName ="保健品"},
                new ItemCate(){ CateId = 50015926, CateName ="珠宝"},
                new ItemCate(){ CateId = 50015926, CateName ="眼镜"},
                new ItemCate(){ CateId = 0, CateName ="手表"},
                new ItemCate(){ CateId = 0, CateName ="运动"},
                new ItemCate(){ CateId = 0, CateName ="户外"},
                new ItemCate(){ CateId = 0, CateName ="乐器"},
                new ItemCate(){ CateId = 0, CateName ="游戏"},
                new ItemCate(){ CateId = 0, CateName ="动漫"},
                new ItemCate(){ CateId = 0, CateName ="影视"},
                new ItemCate(){ CateId = 0, CateName ="美食"},
                new ItemCate(){ CateId = 0, CateName ="生鲜"},
                new ItemCate(){ CateId = 0, CateName ="零食"},
                new ItemCate(){ CateId = 0, CateName ="鲜花"},
                new ItemCate(){ CateId = 0, CateName ="宠物"},
                new ItemCate(){ CateId = 0, CateName ="农资"},
                new ItemCate(){ CateId = 0, CateName ="工具"},
                new ItemCate(){ CateId = 50097129, CateName ="装修"},
                new ItemCate(){ CateId = 0, CateName ="建材"},
                new ItemCate(){ CateId = 0, CateName ="家具"},
                new ItemCate(){ CateId = 50008163, CateName ="家饰"},
                new ItemCate(){ CateId = 50008163, CateName ="家纺"},
                new ItemCate(){ CateId = 56974003, CateName ="汽车"},
                new ItemCate(){ CateId = 56974003, CateName ="二手车"},
                new ItemCate(){ CateId = 0, CateName ="用品"},
                new ItemCate(){ CateId = 0, CateName ="办公"},
                new ItemCate(){ CateId = 0, CateName ="DIY"},
                new ItemCate(){ CateId = 0, CateName ="五金电子"},
                new ItemCate(){ CateId = 0, CateName ="百货"},
                new ItemCate(){ CateId = 0, CateName ="餐厨"},
                new ItemCate(){ CateId = 0, CateName ="家庭保健"},
                new ItemCate(){ CateId = 0, CateName ="学习"},
                new ItemCate(){ CateId = 0, CateName ="卡券"},
                new ItemCate(){ CateId = 50097750, CateName ="本地服务"},
            };

            foreach (var item in cates)
            {
                item.Status = 1;
                ItemCateBLL.Insert(item);
            }

        }

        public static Dictionary<string, Int32> ReadCateFile()
        {
            var path = "D:\\tb\\淘宝网 - 淘！我喜欢.html";
            var text = FileHelper.ReadBinaryFile(path);
            var pattern = "<a.+?href=\"(.+?)\".*>(.+)</a>";
           // var catStr = "cat=(\\d{1,})[&|\"]";
            Dictionary<string, Int32> dict = new Dictionary<string, Int32>(100);
            foreach (Match match in Regex.Matches(text, pattern, RegexOptions.IgnoreCase))
            {
                //var groups = Regex.Matches(match.Groups[1].Value, catStr, RegexOptions.IgnoreCase);
                //if (groups.HasValue())
                //{
                //    var cat = groups[0].Groups[1].Value;
                    if (dict.ContainsKey(match.Groups[2].Value) == false)
                    {
                        dict[match.Groups[2].Value] = 0;
                    }
                //}
                
            }

            var  temp = dict.OrderBy(d => d.Value);
            foreach (var item in temp)
            {
                ItemCate tempItem = new ItemCate() { CateId = item.Value, CateName = item.Key, Status = 1};
                ItemCateBLL.Insert(tempItem);
                Console.WriteLine(item.Key+":"+ item.Value);
            }
            Console.WriteLine("Total:" + temp.Count());
            return dict;
        }
    }
}

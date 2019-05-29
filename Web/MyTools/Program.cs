using AutoMapper;
using BLL;
using BLL.ThirtyPart;
using DotNet.Common;
using Entities;
using Entities.Model;
using MyTools.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
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

            //ImportTaobaoke();
            
            Console.ReadLine();
        }

        private static string[] Cates = new string[] { "女装", "男装", "鞋包", "美妆", "母婴", "食品", "内衣", "数码", "家居用品", "文体车品" };
        public static void ImportTaobaoke()
        {
            PageInfo page = new PageInfo();
            page.PageSize = 100;

            int pageNum = 10;
            foreach (var item in Cates)
            {
                Action<string> actionCoupon = (keyword) =>
                {
                    for (int i = 1; i <= pageNum; i++)
                    {
                        page.PageIndex = i;
                        var result = TaoBaoKe.QueryDgItemCoupon(page, keyword);
                        if (result.Count == 0)
                        {
                            break;
                        }
                        TaoBaoKe.QueryProductDetail(result, 1);
                        TaoBaoKe.QueryProductDetail(result, 2);
                        List<Items> updateItems = new List<Items>(50);
                        foreach (var item2 in result)
                        {
                            var temp = ItemsBLL.GetItem(item2.NumIid);
                            if (temp != null)
                            {
                                temp.Status = 2;
                                updateItems.Add(temp);
                            }
                            else
                            {
                                ItemsBLL.AddCache(item2);
                            }
                        }
                        ItemsBLL.BatchInsert(result);
                        ItemsBLL.BatchUpdate(updateItems);
                    }

                    ItemsBLL.DeleteByStatus();
                };

                Action actionItem = () => {
                    for (int i = 1; i <= pageNum; i++)
                    {
                        var result = TaoBaoKe.QueryItem(page, item);
                        if (result.Count == 0)
                        {
                            break;
                        }
                        TaoBaoKe.QueryProductDetail(result, 1);
                        TaoBaoKe.QueryProductDetail(result, 2);
                        ItemsBLL.BatchInsert(result);
                    }
                };

                actionCoupon(item);

                //TaskParallelHelper.ExecuteTask(actionItem);

            }

        }

        public static void InitCate()
        {
            List<ItemCate> cates = new List<ItemCate>()
            {
                new ItemCate(){ CateId = 16, CateName ="女装"},
                new ItemCate(){ CateId = 50344007, CateName ="男装"},
                new ItemCate(){ CateId = 1625, CateName ="内衣"},
                new ItemCate(){ CateId = 16, CateName ="鞋靴"},
                new ItemCate(){ CateId = 16, CateName ="箱包"},
                new ItemCate(){ CateId = 16, CateName ="配件"},
                new ItemCate(){ CateId = 16, CateName ="童装玩具"},
                new ItemCate(){ CateId = 16, CateName ="孕产"},
                new ItemCate(){ CateId = 16, CateName ="用品"},
                new ItemCate(){ CateId = 16, CateName ="家电"},
                new ItemCate(){ CateId = 16, CateName ="数码"},
                new ItemCate(){ CateId = 16, CateName ="手机"},
                new ItemCate(){ CateId = 16, CateName ="美妆"},
                new ItemCate(){ CateId = 16, CateName ="洗护"},
                new ItemCate(){ CateId = 16, CateName ="保健品"},
                new ItemCate(){ CateId = 16, CateName ="珠宝"},
                new ItemCate(){ CateId = 16, CateName ="眼镜"},
                new ItemCate(){ CateId = 16, CateName ="手表"},
                new ItemCate(){ CateId = 16, CateName ="运动"},
                new ItemCate(){ CateId = 16, CateName ="户外"},
                new ItemCate(){ CateId = 16, CateName ="乐器"},
                new ItemCate(){ CateId = 16, CateName ="游戏"},
                new ItemCate(){ CateId = 16, CateName ="动漫"},
                new ItemCate(){ CateId = 16, CateName ="影视"},
                new ItemCate(){ CateId = 16, CateName ="美食"},
                new ItemCate(){ CateId = 16, CateName ="生鲜"},
                new ItemCate(){ CateId = 16, CateName ="零食"},
                new ItemCate(){ CateId = 16, CateName ="鲜花"},
                new ItemCate(){ CateId = 16, CateName ="宠物"},
                new ItemCate(){ CateId = 16, CateName ="农资"},
                new ItemCate(){ CateId = 16, CateName ="工具"},
                new ItemCate(){ CateId = 50097129, CateName ="装修"},
                new ItemCate(){ CateId = 16, CateName ="建材"},
                new ItemCate(){ CateId = 16, CateName ="家具"},
                new ItemCate(){ CateId = 50008163, CateName ="家饰"},
                new ItemCate(){ CateId = 16, CateName ="家纺"},
                new ItemCate(){ CateId = 16, CateName ="汽车"},
                new ItemCate(){ CateId = 16, CateName ="二手车"},
                new ItemCate(){ CateId = 16, CateName ="用品"},
                new ItemCate(){ CateId = 16, CateName ="办公"},
                new ItemCate(){ CateId = 16, CateName ="DIY"},
                new ItemCate(){ CateId = 16, CateName ="五金电子"},
                new ItemCate(){ CateId = 16, CateName ="百货"},
                new ItemCate(){ CateId = 16, CateName ="餐厨"},
                new ItemCate(){ CateId = 16, CateName ="家庭保健"},
                new ItemCate(){ CateId = 16, CateName ="学习"},
                new ItemCate(){ CateId = 16, CateName ="卡券"},
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
            var catStr = "cat=(\\d{1,})[&|\"]";
            Dictionary<string, Int32> dict = new Dictionary<string, Int32>(100);
            foreach (Match match in Regex.Matches(text, pattern, RegexOptions.IgnoreCase))
            {
                var groups = Regex.Matches(match.Groups[1].Value, catStr, RegexOptions.IgnoreCase);
                if (groups.HasValue())
                {
                    var cat = groups[0].Groups[1].Value;
                    if (dict.ContainsKey(match.Groups[2].Value) == false)
                    {
                        dict[match.Groups[2].Value] = cat.TryToInt();
                    }
                }
                
            }

            var  temp = dict.OrderBy(d => d.Value);
            foreach (var item in temp)
            {
                ItemCate tempItem = new ItemCate() { CateId = item.Value, CateName = item.Key, Status = 1};
                ItemCateBLL.Insert(tempItem);
                Console.WriteLine(item.Key+":"+ item.Value);
            }
            return dict;
        }
    }
}

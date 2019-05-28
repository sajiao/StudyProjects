using AutoMapper;
using BLL;
using DotNet.Common;
using Entities;
using Entities.Model;
using MyTools.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ThirtyPart;

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
            DAL.DB.GetDB();

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
                Action actionCoupon = () =>
                {
                    for (int i = 1; i <= pageNum; i++)
                    {
                        page.PageIndex = i;
                        var result = TaoBaoKe.QueryDgItemCoupon(page, item);
                        if (result.Count == 0)
                        {
                            break;
                        }
                        TaoBaoKe.QueryProductDetail(result,1);
                        TaoBaoKe.QueryProductDetail(result, 2);
                        ItemsBLL.BatchInsert(result);
                    }
                };

                Action actionItem = ()=> {
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

                TaskParallelHelper.ExecuteTask(actionCoupon);
                TaskParallelHelper.ExecuteTask(actionItem);
 
            }

        }
    }
}

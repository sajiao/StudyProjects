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

            var files = Directory.GetFiles("D:\\tb\\", "*.xls");

            foreach (var item in files)
            {
                if (item.Contains("done"))
                {
                    continue;
                }

                if (item.Contains("聚划算拼团单品"))
                {
                    TaoBaoImport.ImportJuTuan(item);
                }
                else if (item.Contains("精选优质商品清单"))
                {
                     TaoBaoImport.ImportItems(item);
                }

            }

            // var temp =  ObjectXmlSerializer.LoadFromXml<WordBook>(path);
            //var xmlPaths = "D:\\BaiduNetdiskDownload\\COCAXML";
            //WordImport.Import(xmlPaths);

            //PageInfo page = new PageInfo();
            //page.PageSize = 100;


            //for (int i = 1; i <= 10; i++)
            //{
            //    page.PageIndex = i;
            //    var result2 = TaoBaoKe.QueryDgItemCoupon(page);
            //    ItemsBLL.BatchInsert(result2);

            //    var result = TaoBaoKe.QueryItem(page);
            //    ItemsBLL.BatchInsert(result);

            //}


            Console.ReadLine();
        } 
    }
}

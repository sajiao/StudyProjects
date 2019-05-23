using DotNet.Common;
using Entities.Model;
using MyTools.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            DAL.DB.GetDB();

            //var files = Directory.GetFiles("D:\\tb\\", "*.xls");

            //foreach (var item in files)
            //{
            //    if (item.Contains("聚划算拼团单品"))
            //    {
            //       // TaoBaoImport.ImportJuTuan(item);
            //    }
            //    else if (item.Contains("精选优质商品清单"))
            //    {
            //        //  TaoBaoImport.ImportCouponGood(item);
            //        //LoadFromXml TaoBaoImport.ImportItems(item);
            //    }

            //}

            // var temp =  ObjectXmlSerializer.LoadFromXml<WordBook>(path);
            var xmlPaths = "D:\\BaiduNetdiskDownload\\COCAXML";
            WordImport.Import(xmlPaths);

            Console.ReadLine();
        }
    }
}

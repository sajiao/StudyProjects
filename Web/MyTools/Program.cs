using Entities.Model;
using MyTools.Goods;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyTools
{
    class Program
    {
        static void Main(string[] args)
        {
            //DAL.DB.GetDB();

            var files = Directory.GetFiles("D:\\tb\\", "*.xls");

            foreach (var item in files)
            {
                if (item.Contains("聚划算拼团单品"))
                {
                    TaoBaoImport.ImportJuTuan(item);
                }
                else if (item.Contains("精选优质商品清单"))
                {
                    TaoBaoImport.ImportCouponGood(item);
                } 
                
            }

            Console.ReadLine();
        }
    }
}

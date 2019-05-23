using DotNet.Common;
using DotNet.Common.Attributes;
using DotNet.Common.Helper;
using Entities.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyTools.Import
{
   public static class TaoBaoImport
    {
        public static void ImportItems(string path)
        {
            Items t = new Items();
            var list = TaoBaoImport.Import(t, path);
            var tempList = new List<Items>(500);
            foreach (var item in list)
            {
                item.Uid = 1;
                item.CateId = 1;
                item.OrdId = 1;
                item.IsShow = 1;
                item.AliId = "1";
                item.CateId = 1;
                item.Status = 1;
                item.Tags = item.TypeName.Replace("/", ",");
                tempList.Add(item);
                if (tempList.Count == 500)
                {
                    BLL.ItemsBLL.BatchInsert(tempList);
                    tempList.Clear();
                }
            }

            if (tempList.Count > 0)
            {
                BLL.ItemsBLL.BatchInsert(tempList);

            }

            Console.WriteLine("ImportCouponGood done, total count:" + list.Count.ToString());
        }


        public static void ImportCouponGood(string path)
        {
            CouponGood t = new CouponGood();
            var list = TaoBaoImport.Import(t, path);
            var tempList = new List<CouponGood>(500);
            foreach (var item in list)
            {
                item.Id = Guid.NewGuid();
                item.CreatedTime = DateTime.Now;
                item.Tags = item.TypeName.Replace("/", ",");
                tempList.Add(item);
                if (tempList.Count == 500)
                {
                    BLL.CouponGoodBLL.BatchInsert(tempList);
                    tempList.Clear();
                }
            }

            if (tempList.Count > 0)
            {
                BLL.CouponGoodBLL.BatchInsert(tempList);

            }

            Console.WriteLine("ImportCouponGood done, total count:" + list.Count.ToString());
        }

        public static void ImportJuTuan(string path)
        {
            JuTuan t = new JuTuan();
            var list = TaoBaoImport.Import(t, path);
            var tempList = new List<JuTuan>(500);
            foreach (var item in list)
            {
                item.Id = Guid.NewGuid();
                item.CreatedTime = DateTime.Now;
                item.Tags = item.TypeName.Replace("/", ",");
                tempList.Add(item);
                if (tempList.Count == 500)
                {
                    BLL.JuTuanBLL.BatchInsert(tempList);
                    tempList.Clear();
                }
            }

            if (tempList.Count > 0)
            {
                BLL.JuTuanBLL.BatchInsert(tempList);

            }

            Console.WriteLine("ImportJuTuan done, total count:" + list.Count.ToString());
        }

        public static List<T> Import<T>(T t,string path)
        {
            DataTable dt = OfficeHelper.ReadExcelToDataTable(path);
            var lst = new List<T>(1000);
            System.Type tt = t.GetType();//获取指定名称的类型
            object ff = Activator.CreateInstance(tt, null);//创建指定类型实例
            PropertyInfo[] fields = ff.GetType().GetProperties();//获取指定对象的所有公共属性
            List<MapNameAttribute> attributes = new List<MapNameAttribute>(20);
            foreach (var item in fields)
            {
                var temps = item.GetCustomAttributes(typeof(MapNameAttribute), true);
                if (temps != null)
                {
                    foreach (var temp in temps)
                    {
                        attributes.Add(temp as MapNameAttribute);
                    }
                   
                }
            }

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                object obj = Activator.CreateInstance(tt, null);
                var dr = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var column = dt.Columns[j];
                    var columnMap = attributes.FirstOrDefault(a => a.MapName == column.ColumnName.TryTrim());
                    // 没找到匹配的字段就忽略
                    if (columnMap == null)
                    {
                        continue;
                    }
                    foreach (PropertyInfo p in fields)
                    {
                        if (columnMap.Name != p.Name)
                        {
                            continue;
                        }
                        p.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], p.PropertyType), null);//给对象赋值
                    }
                }

                lst.Add((T)obj);
            }
            return lst;
        }
    }
}

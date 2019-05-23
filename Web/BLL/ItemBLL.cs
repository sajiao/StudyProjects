using DAL;
using Entities;
using Entities.Model;
using Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DotNet.Common;
using SqlSugar;
using System.Diagnostics;

namespace BLL
{
    public class ItemsBLL : DbContext
    {
        public static Items GetById(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ItemsDb.GetById(id);
        }

        public static List<Items> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.ItemsDb.GetList();
        }

        //public static Result<Items> QueryPageList(ReqItems req)
        //{
        //    var dbContext = new DbContext();
        //    Expression<Func<Items, bool>> fun = null;
        //    if (req.GoodName.IsNotNullOrEmpty())
        //    {
        //        fun = (r) => SqlFunc.Contains(r.Title, req.GoodName);
        //    }

        //    var result = dbContext.ItemsDb.GetPages(req.ConvertData(), fun, req.PageInfo);
        //    return result;
        //}

        public static Items Insert(Items param)
        {
            var dbContext = new DbContext();
            var id = dbContext.ItemsDb.InsertReturnIdentity(param);
            return GetById(param.Id);
        }

        public static void BatchInsert(List<Items> param)
        {
            if (param == null || param.Count == 0)
            {
                return;
            }

            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var dbContext = new DbContext();
                dbContext.ItemsDb.InsertRange(param);
                sw.Stop();
                Logger.WriteProcessLog(string.Format("ItemsBLL.BatchInsert共计：{0} 条数据，耗时:{0} ms", param.Count, sw.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }

        public static Items Update(Items param)
        {
            var dbContext = new DbContext();
            dbContext.ItemsDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ItemsDb.DeleteById(id);
        }
    }
}

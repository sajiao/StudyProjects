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
    public class JuTuanBLL : DbContext
    {
        public static JuTuan GetById(Guid id)
        {
            var dbContext = new DbContext();
            return dbContext.JuTuanDb.GetById(id);
        }

        public static List<JuTuan> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.JuTuanDb.GetList();
        }

        public static Result<JuTuan> QueryPageList(ReqJuTuan req)
        {
            var dbContext = new DbContext();
            Expression<Func<JuTuan, bool>> fun = null;
            if (req.GoodName.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.GoodName, req.GoodName);
            }

            var result = dbContext.JuTuanDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            return result;
        }

        public static JuTuan Insert(JuTuan param)
        {
            var dbContext = new DbContext();
            var id = dbContext.JuTuanDb.InsertReturnIdentity(param);
            return GetById(param.Id);
        }

        public static void BatchInsert(List<JuTuan> param)
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
                dbContext.JuTuanDb.InsertRange(param);
                sw.Stop();
                Logger.WriteProcessLog(string.Format("JuTuanBLL.BatchInsert共计：{0} 条数据，耗时:{0} ms", param.Count, sw.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }


        public static JuTuan Update(JuTuan param)
        {
            var dbContext = new DbContext();
            dbContext.JuTuanDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.JuTuanDb.DeleteById(id);
        }
    }
}

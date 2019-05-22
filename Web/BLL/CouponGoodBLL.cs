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
   public class CouponGoodBLL : DbContext
    {
        public static CouponGood GetById(Guid id)
        {
            var dbContext = new DbContext();
            return dbContext.CouponGoodDb.GetById(id);
        }

        public static List<CouponGood> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.CouponGoodDb.GetList();
        }

        public static Result<CouponGood> QueryPageList(ReqCouponGood req)
        {
            var dbContext = new DbContext();
            Expression<Func<CouponGood, bool>> fun = null;
            if (req.GoodName.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.GoodName, req.GoodName);
            }

            var result = dbContext.CouponGoodDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            return result;
        }

        public static CouponGood Insert(CouponGood param)
        {
            var dbContext = new DbContext();
            var id = dbContext.CouponGoodDb.InsertReturnIdentity(param);
            return GetById(param.Id);
        }

        public static void BatchInsert(List<CouponGood> param)
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
                dbContext.CouponGoodDb.InsertRange(param);
                sw.Stop();
                Logger.WriteProcessLog(string.Format("JuTuanBLL.BatchInsert共计：{0} 条数据，耗时:{0} ms", param.Count, sw.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }

        public static CouponGood Update(CouponGood param)
        {
            var dbContext = new DbContext();
            dbContext.CouponGoodDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.CouponGoodDb.DeleteById(id);
        }
    }
}

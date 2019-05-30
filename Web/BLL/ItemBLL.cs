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
using System.Linq;
using BLL.ThirtyPart;

namespace BLL
{
    public class ItemsBLL : DbContext
    {
        private static List<Items> mDict = null;

        static ItemsBLL()
        {
            mDict = GetAll();
        }

        public static Items GetItem(Int64 id)
        {
          return  mDict.FirstOrDefault(m=>m.NumIid == id);
        }

        private static void Delete(Items item)
        {
            if (mDict.Contains(item))
            {
                mDict.Remove(item);
            }
        }

        public static Items GetById(Int64 id)
        {
            var dbContext = new DbContext();
            return dbContext.ItemsDb.GetById(id);
        }

        public static List<Items> GetAll()
        {
            var dbContext = new DbContext();
            Expression<Func<Items, bool>> fun = null;
            fun = (r) => r.Status == 1 && r.EndTime > DateTime.Now.AddMonths(-2) && r.EndTime < DateTime.Now;
            
            return dbContext.ItemsDb.GetList(fun);
        }

        public static Result<Items> QueryPageList(ReqItems req)
        {
            Result<Items> results = new Result<Items>();
            if (req.IsFull)
            {
                var tempItems = TaoBaoKeHelper.QueryCoupon(req.KeyWord);
                if (tempItems.Count > req.PageInfo.PageSize)
                {
                    results.Results = tempItems.Take(req.PageInfo.PageSize).ToList();
                }
                results.TotalCount = tempItems.Count;
                return results;
            }
            if (mDict.HasValue())
            {
                List<Items> temp = mDict.Where(m =>
               {
                   bool tempResult = m.Status == 1;
                   if (tempResult && req.KeyWord.IsNotNullOrEmpty())
                   {
                       tempResult = m.Title.Contains(req.KeyWord);

                       if (tempResult == false)
                       {
                           tempResult = m.Tags.Contains(req.KeyWord);
                       }

                       if (tempResult == false)
                       {
                           tempResult = m.ProductUrl.Contains(req.KeyWord);
                       }

                       if (tempResult == false)
                       {
                           tempResult = m.ProductWapUrl.Contains(req.KeyWord);
                       }
                   }

                   if (tempResult && req.TypeId > 0)
                   {
                       tempResult = m.TypeId == req.TypeId;
                   }

                   if (tempResult&& req.ZCId > 0)
                   {
                       tempResult = m.ZCId == req.ZCId;
                   }

                   if (tempResult && req.Tag.IsNotNullOrEmpty())
                   {
                       tempResult = m.Tags.Contains(req.Tag) || m.Title.Contains(req.Tag);
                   }

                   return tempResult;

               }).ToList();

                temp = temp.OrderByDescending(t => t.YouhuiPrice).ToList();

                if (req.PageInfo.SortFields.IsNotNullOrEmpty() && req.PageInfo.SortFields == "volume")
                {
                    if (req.PageInfo.Sort.EqualsCurrentCultureIgnoreCase("desc"))
                    {
                        temp = temp.OrderByDescending(t => t.Volume).ToList();
                    }
                    else
                    {
                        temp = temp.OrderBy(t => t.Volume).ToList();
                    }
                  
                }
               results.Results= temp.Skip(req.PageInfo.PageSize * (req.PageInfo.PageIndex - 1)).Take(req.PageInfo.PageSize).ToList();
                results.TotalCount = temp.Count;
                if (results.TotalCount == 0)
                {
                    var tempItems = TaoBaoKeHelper.QueryCoupon(req.KeyWord);
                    if (tempItems.Count > req.PageInfo.PageSize)
                    {
                        results.Results = tempItems.Take(req.PageInfo.PageSize).ToList();
                    }
                    results.TotalCount = tempItems.Count;
                }
                return results;
            }

            var dbContext = new DbContext();
            Expression<Func<Items, bool>> fun = null;
            if (req.KeyWord.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.Title, req.KeyWord);
            }

            var result = dbContext.ItemsDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            return result;
        }

        public static Items Insert(Items param)
        {
            var dbContext = new DbContext();
            var id = dbContext.ItemsDb.InsertReturnIdentity(param);
            return GetById(param.Id);
        }

        public static void AddCache(Items param)
        {
            if (param == null)
            {
                return;
            }
         
            if (GetItem(param.NumIid) == null)
            {
                mDict.Add(param);
            }
        }

        public static void UpdateCache(List<Items> param)
        {
            if (param == null)
            {
                return;
            }
            foreach (var item in param)
            {
                if (GetItem(item.NumIid) == null)
                {
                    continue;
                }
                mDict.Remove(item);
            }
            
            mDict.AddRange(param);
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

        public static void BatchUpdate(List<Items> param)
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
                dbContext.ItemsDb.UpdateRange(param);
                sw.Stop();
                Logger.WriteProcessLog(string.Format("ItemsBLL.BatchUpdate：{0} 条数据，耗时:{0} ms", param.Count, sw.ElapsedMilliseconds));
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

        public static bool DeleteByStatus()
        {
            var dbContext = new DbContext();
            Expression<Func<Items, bool>> fun = null;
            fun = (r) => r.Status == 2;
            return dbContext.ItemsDb.Delete(fun);
        }
    }
}

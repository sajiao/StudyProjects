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
using System.Threading.Tasks;

namespace BLL
{
    public class ItemsBLL : DbContext
    {
        private static List<Items> mDict = new List<Items>();

        private static object LockObject = new object();

        static ItemsBLL()
        {
            lock (LockObject)
            {
                mDict = GetAll();
            }
        }

        public static void Refresh()
        {
            var temp = GetAll();

            lock (LockObject)
            {
                mDict = temp;
            }
        }


        public static List<Items> GetData()
        {
            return mDict;
        }

        public static void ClearSameData()
        {
            PageInfo page = new PageInfo();
            page.PageIndex = 0;
            page.PageSize = 1000;
            page.SortFields = "NumIid";
            long totalPage = 100;
            while (totalPage > 0)
            {
                var dbContext = new DbContext();
                var req = new Items();
                req.Status = 1;

                page.PageIndex++;
               var data = dbContext.ItemsDb.GetPages(new Items(), null, page);
                totalPage = data.TotalCount / page.PageSize + 1;
                if (data.TotalCount == 0 || data.Results == null)
                {
                    break;
                }
                if (data.Results.Count < page.PageSize)
                {
                    totalPage = 0;
                }
                
                var tempData = data.Results.OrderByDescending(d => d.LastTime).ToList();
                List<Items> newData = new List<Items>(1000);
                List<Items> deleteData = new List<Items>(500);
                foreach (var item in tempData)
                {
                    if (newData.Exists(d => d.NumIid == item.NumIid))
                    {
                        item.Status = 2;
                        deleteData.Add(item);
                        continue;
                    }
                    else
                    {
                        newData.Add(item);
                    }
                    if (deleteData.Count >= 500)
                    {
                        BatchUpdate(deleteData);
                        deleteData.Clear();
                    }
                }

                if (deleteData.Count > 0)
                {
                    BatchUpdate(deleteData);
                }
            }
            
            DeleteByStatus();
        }

        public static Items GetItem(Int64 id)
        {
          return  mDict.FirstOrDefault(m=> m.NumIid == id);
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
            fun = (r) => r.Status == 1;
            List<Items> temp = new List<Items>(50000);
            PageInfo page = new PageInfo();
            page.PageIndex = 0;
            page.PageSize = 10000;
            bool isRun = true;
            while (isRun)
            {
                page.PageIndex++;
                var data = dbContext.ItemsDb.GetPages(new Items(), fun, page);
                
                if (data.TotalCount == 0 || data.Results == null)
                {
                    isRun = false;
                    break;
                }
                if (data.Results.Count < page.PageSize)
                {
                    isRun = false;
                }
                temp.AddRange(data.Results);
            }

            return temp;
        }

        public static List<Items> GetByTag(string tag)
        {
            var dbContext = new DbContext();
            Expression<Func<Items, bool>> fun = null;
            fun = (r) => r.Tags.Contains(tag);

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
                    results.Results = tempItems.OrderByDescending(t => t.CommissionRate).ThenByDescending(t => t.Volume).Skip(req.PageInfo.PageSize * (req.PageInfo.PageIndex - 1)).Take(req.PageInfo.PageSize).ToList();
                }
                else
                {
                    results.Results = tempItems;
                }
                results.TotalCount = tempItems.Count;
                Task.Factory.StartNew(() =>
                {
                    UpdateCache(tempItems);
                });
                return results;
            }
            if (mDict.HasValue())
            {
                List<Items> temp = mDict.Where(m =>
               {
                   bool tempResult = m.Status == 1;
                   if (tempResult && req.KeyWord.IsNotNullOrEmpty())
                   {
                       tempResult = m.Title.TryContains(req.KeyWord);

                       if (tempResult == false)
                       {
                           tempResult = m.Tags.TryContains(req.KeyWord);
                       }

                       if (tempResult == false)
                       {
                           tempResult = m.ProductUrl.TryContains(req.KeyWord);
                       }

                       if (tempResult == false )
                       {
                           tempResult = m.ProductWapUrl.TryContains(req.KeyWord);
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

                   if (tempResult && req.Tag.IsNotNullOrEmpty() && m.Tags != null)
                   {
                       tempResult = m.Tags.TryContains(req.Tag) || m.Title.TryContains(req.Tag);
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
                        results.Results = tempItems.OrderByDescending(t=> t.CommissionRate).ThenByDescending(t => t.Volume).Take(req.PageInfo.PageSize).ToList();
                    }
                    results.TotalCount = tempItems.Count;
                    Task.Factory.StartNew(() =>
                    {
                        UpdateCache(tempItems);
                    });
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
            lock (LockObject)
            {
                foreach (var item in param)
                {
                    var temp = GetItem(item.NumIid);
                    if (temp == null)
                    {
                        continue;
                    }
                    mDict.Remove(temp);
                }

                mDict.AddRange(param);
            }
            
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

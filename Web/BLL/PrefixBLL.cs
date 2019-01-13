using DAL;
using Entities;
using Entities.Request;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNet.Common;

namespace BLL
{
    public class PrefixBLL : DbContext, IInits
    {
        private static Dictionary<int, Prefix> mDict = null;
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, Prefix>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, Prefix>();
            }
        }

        private static Prefix GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static Prefix GetById(int id)
        {
            return GetItem(id);
        }

        public static (bool, Prefix) GetByName(string word)
        {
            Prefix result = null;
            bool isExist = false;
            foreach (var item in mDict)
            {
                if (item.Value.Word.EqualsCurrentCultureIgnoreCase(word))
                {
                    result = item.Value;
                    isExist = true;
                    break;
                }
            }
            return (isExist, result);
        }

        public static List<Prefix> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.PrefixDb.GetList();
        }

        public static Result<Prefix> QueryPageList(ReqPrefix req)
        {
            var dbContext = new DbContext();
            Expression<Func<Prefix, bool>> fun = null;
            if (req.Word.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.Word, req.Word);
            }

            var result = dbContext.PrefixDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            return result;
        }

        public static Prefix Insert(Prefix param)
        {
            var (isExist, result) = GetByName(param.Word);
            int id;
            if (isExist)
            {
                id = result.Id;
                param.Id = result.Id;
                Update(param);
            }
            else
            {
                var dbContext = new DbContext();
                id = dbContext.PrefixDb.InsertReturnIdentity(param);
            }

            return GetById(id);
        }

        public static Prefix Update(Prefix param)
        {
            var dbContext = new DbContext();
            dbContext.PrefixDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.PrefixDb.DeleteById(id);
        }
    }
}

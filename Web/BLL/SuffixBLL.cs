using DAL;
using Entities;
using Entities.Request;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DotNet.Common;


namespace BLL
{
   public class SuffixBLL : DbContext, IInits
    {
        private static Dictionary<int, Suffix> mDict = null;
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, Suffix>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, Suffix>();
            }
        }

        private static Suffix GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static Suffix GetById(int id)
        {
            return GetItem(id);
        }

        public static (bool, Suffix) GetByName(string word)
        {
            Suffix result = null;
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

        public static List<Suffix> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.SuffixDb.GetList();
        }

        public static Result<Suffix> QueryPageList(ReqSuffix req)
        {
            var dbContext = new DbContext();
            Expression<Func<Suffix, bool>> fun = null;
            if (req.Word.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.Word, req.Word);
            }

            var result = dbContext.SuffixDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            return result;
        }

        public static Suffix Insert(Suffix param)
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
                id = dbContext.SuffixDb.InsertReturnIdentity(param);
            }

            return GetById(id);
        }

        public static Suffix Update(Suffix param)
        {
            var dbContext = new DbContext();
            dbContext.SuffixDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.SuffixDb.DeleteById(id);
        }

    }
}

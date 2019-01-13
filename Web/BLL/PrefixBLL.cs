using DAL;
using Entities;
using Entities.Request;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNet.Common;
using Newtonsoft.Json;

namespace BLL
{
    public class PrefixBLL : DbContext, IInits
    {
        private static Dictionary<string, Prefix> mDict = null;
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<string, Prefix>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Word] = item;
                }
            }
            else
            {
                mDict = new Dictionary<string, Prefix>();
            }
        }

        private static Prefix GetItem(string word)
        {
            if (mDict.ContainsKey(word))
            {
                return mDict[word];
            }

            return null;
        }

        public static Prefix GetById(int id)
        {
            Prefix result = null;
            foreach (var item in mDict)
            {
                if (item.Value.Id == id)
                {
                    result = item.Value;

                    break;
                }
            }
            return result;
        }

        public static (bool, Prefix) GetByName(string word)
        {
            Prefix result = GetItem(word);
            bool isExist = result != null;
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

            if (result.Results != null)
            {
                result.Results.ForEach(r => {
                    if (r.Json.IsNotNullOrEmpty())
                    {
                        r.Extensions = JsonConvert.DeserializeObject<List<FixExtension>>(r.Json);
                    }
                });
            }

            return result;
        }

        public static Prefix Insert(Prefix param)
        {
            var (isExist, result) = GetByName(param.Word);
            int id;
            param.Extensions = CommonHelper.GetWord(param.Json);
            param.Json = JsonConvert.SerializeObject(param.Extensions);
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
                param.Id = id;
            }
            mDict[param.Word] = param;
            return GetById(id);
        }

        public static Prefix Update(Prefix param)
        {
            param.Extensions = CommonHelper.GetWord(param.Json);
            param.Json = JsonConvert.SerializeObject(param.Extensions);
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

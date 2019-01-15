using DAL;
using Entities;
using Entities.Request;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DotNet.Common;
using Newtonsoft.Json;
using Entities.Model;

namespace BLL
{
   public class SuffixBLL : DbContext, IInits
    {
        private static Dictionary<string, Suffix> mDict = null;
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<string, Suffix>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Word] = item;
                }
            }
            else
            {
                mDict = new Dictionary<string, Suffix>();
            }
        }

        private static Suffix GetItem(string word)
        {
            if (mDict.ContainsKey(word))
            {
                return mDict[word];
            }

            return null;
        }

        public static Suffix GetById(int id)
        {
            Suffix result = null;
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

        public static (bool, Suffix) GetByName(string word)
        {
            Suffix result = GetItem(word);
            bool isExist = result != null;
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

        public static Suffix Insert(Suffix param)
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
                id = dbContext.SuffixDb.InsertReturnIdentity(param);
                param.Id = id;
            }
            mDict[param.Word] = param;
            return GetById(id);
        }

        public static Suffix Update(Suffix param)
        {
            param.Extensions = CommonHelper.GetWord(param.Json);
            param.Json = JsonConvert.SerializeObject(param.Extensions);
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

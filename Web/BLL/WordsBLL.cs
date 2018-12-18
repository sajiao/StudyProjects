using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Common;

namespace BLL
{
   public class WordsBLL : DbContext, IInits
    {
        private static Dictionary<int, Words> mDict = null;
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, Words>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, Words>();
            }
        }

        private static Words GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static Words GetById(int id)
        {
            return GetItem(id);
        }

        public static (bool, Words) GetByName(string word)
        {
            Words result = null;
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

        public static List<Words> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.WordsDb.GetList();
        }

        public static Words Insert(Words param)
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
                id = dbContext.WordsDb.InsertReturnIdentity(param);
            }

            return GetById(id);
        }

        public static Words Update(Words param)
        {
            var dbContext = new DbContext();
            dbContext.WordsDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.WordsDb.DeleteById(id);
        }
    }
}

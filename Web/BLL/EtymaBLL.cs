using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Common;
namespace BLL
{
   public class EtymaBLL : DbContext, IInits
    {
        private static Dictionary<int, Etyma> mDict = null;
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, Etyma>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, Etyma>();
            }
        }

        private static Etyma GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static Etyma GetById(int id)
        {
            return GetItem(id);
        }

        public static (bool,Etyma) GetByName(string word)
        {
            Etyma result = null;
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

        public static List<Etyma> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.EtymaDb.GetList();
        }

        public static Etyma Insert(Etyma param)
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
                id = dbContext.EtymaDb.InsertReturnIdentity(param);
            }
            
            return GetById(id);
        }

        public static Etyma Update(Etyma param)
        {
            var dbContext = new DbContext();
            dbContext.EtymaDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.EtymaDb.DeleteById(id);
        }
    }
}

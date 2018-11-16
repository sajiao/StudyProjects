using DAL;
using Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ModuleSubBLL : DbContext, IInits
    {
        private static Dictionary<int, DBModuleSub> mDict = null;

        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, DBModuleSub>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, DBModuleSub>();
            }
        }

        private static DBModuleSub GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static DBModuleSub GetById(int id)
        {
            return GetItem(id);
        }

        public static List<DBModuleSub> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.ModuleSubDb.GetList();
        }

        public static DBModuleSub Insert(DBModuleSub param)
        {
            var dbContext = new DbContext();
            var id = dbContext.ModuleSubDb.InsertReturnIdentity(param);
            return GetById(id);
        }

        public static DBModuleSub Update(DBModuleSub param)
        {
            var dbContext = new DbContext();
            dbContext.ModuleSubDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ModuleSubDb.DeleteById(id);
        }
    }
}

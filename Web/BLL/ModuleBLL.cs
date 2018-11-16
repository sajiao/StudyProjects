using DAL;
using Entities.DB;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ModuleBLL: DbContext, IInits
    {
        private static Dictionary<int, DBModule> moduleDict = null;

        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                moduleDict = new Dictionary<int, DBModule>(all.Count);
                foreach (var item in all)
                {
                    moduleDict[item.Id] = item;
                }
            }
            else
            {
                moduleDict = new Dictionary<int, DBModule>();
            }
        }

        public static List<DBModule> GetAll()
        {
           var dbContext = new DbContext();
           return dbContext.ModuleDb.GetList();
        }

        public static DBModule GetById(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ModuleDb.GetById(id);
        }

        public static DBModule Insert(DBModule param)
        {
            var dbContext = new DbContext();
            var id = dbContext.ModuleDb.InsertReturnIdentity(param);
            return GetById(id);
        }
    }
}

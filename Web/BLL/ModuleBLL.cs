﻿using DAL;
using Entities.DB;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ModuleBLL: DbContext, IInits
    {
        private static Dictionary<int, DBModule> mDict = null;

        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, DBModule>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, DBModule>();
            }
        }

        private static DBModule GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static DBModule GetById(int id)
        {
            return GetItem(id);
        }

        public static List<DBModule> GetAll()
        {
           var dbContext = new DbContext();
           return dbContext.ModuleDb.GetList();
        }

        public static DBModule Insert(DBModule param)
        {
            var dbContext = new DbContext();
            var id = dbContext.ModuleDb.InsertReturnIdentity(param);
            return GetById(id);
        }

        public static DBModule Update(DBModule param)
        {
            var dbContext = new DbContext();
            dbContext.ModuleDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ModuleDb.DeleteById(id);
        }
    }
}

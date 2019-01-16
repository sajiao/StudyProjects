using DAL;
using Entities;
using System;
using System.Collections.Generic;
using Entities.Model;

namespace BLL
{
    public class ModuleSubBLL : DbContext, IInits
    {
        private static Dictionary<int, ModuleSub> mDict = null;

        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, ModuleSub>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, ModuleSub>();
            }
        }

        private static ModuleSub GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static ModuleSub GetById(int id)
        {
            return GetItem(id);
        }

        public static List<ModuleSub> GetByModuleId(int moduleID)
        {
            List<ModuleSub> result = new List<ModuleSub>();
            foreach (var item in mDict)
            {
                if (moduleID == 0)
                {
                    result.Add(item.Value);
                }
                else if (item.Value.ModuleId == moduleID)
                {
                    result.Add(item.Value);

                    break;
                }
            }

            return result;
        }

        public static List<ModuleSub> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.ModuleSubDb.GetList();
        }

        public static ModuleSub Insert(ModuleSub param)
        {
            ModuleSub result;
            var item = GetById(param.Id);
            if (item == null)
            {
                var dbContext = new DbContext();
                dbContext.ModuleSubDb.InsertReturnIdentity(param);
                result = GetById(param.Id);
                mDict[param.Id] = result;
            }
            else
            {
                result = Update(param);
            }
            
            return result;
        }

        public static ModuleSub Update(ModuleSub param)
        {
            var dbContext = new DbContext();
            dbContext.ModuleSubDb.Update(param);
            var reslut = GetById(param.Id);
            mDict[param.Id] = reslut;
            return reslut;
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ModuleSubDb.DeleteById(id);
        }
    }
}

﻿using DAL;
using Entities;
using System;
using System.Collections.Generic;
using Entities.Model;

namespace BLL
{
    public class ModuleBLL: DbContext, IInits
    {
        private static Dictionary<int, Module> mDict = null;

        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mDict = new Dictionary<int, Module>(all.Count);
                foreach (var item in all)
                {
                    mDict[item.Id] = item;
                }
            }
            else
            {
                mDict = new Dictionary<int, Module>();
            }
        }

        private static Module GetItem(int id)
        {
            if (mDict.ContainsKey(id))
            {
                return mDict[id];
            }

            return null;
        }

        public static Module GetById(int id)
        {
            return GetItem(id);
        }

        public static List<Module> GetAll()
        {
           var dbContext = new DbContext();
           return dbContext.ModuleDb.GetList();
        }

        public static Module Insert(Module param)
        {
            Module result;
            var item = GetById(param.Id);
            if (item == null)
            {
                var dbContext = new DbContext();
                dbContext.ModuleDb.InsertReturnIdentity(param);
                result = GetById(param.Id);
                mDict[param.Id] = result;
            }
            else
            {
                result = Update(param);
            }
           
            return result;
        }

        public static Module Update(Module param)
        {
            var dbContext = new DbContext();
            dbContext.ModuleDb.Update(param);
            var reslut = GetById(param.Id);
            mDict[param.Id] = reslut;
            return reslut;
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ModuleDb.DeleteById(id);
        }
    }
}

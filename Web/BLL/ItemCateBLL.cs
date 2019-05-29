using DAL;
using Entities;
using Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DotNet.Common;
using SqlSugar;
using System.Diagnostics;
using System.Linq;
using Entities.Model;
namespace BLL
{
    public class ItemCateBLL : DbContext
    {
        private static List<ItemCate> mDict = null;

        static ItemCateBLL()
        {
            mDict = GetAll();
        }

        public static ItemCate GetItem(Int32 id)
        {
            return mDict.FirstOrDefault(m => m.CateId == id);
        }

        private static void Delete(ItemCate item)
        {
            if (mDict.Contains(item))
            {
                mDict.Remove(item);
            }
        }

        public static ItemCate GetById(Int32 id)
        {
            var dbContext = new DbContext();
            return dbContext.ItemCateDb.GetById(id);
        }

        public static List<ItemCate> GetAll()
        {
            var dbContext = new DbContext();
            //Expression<Func<ItemCate, bool>> fun = null;
            //fun = (r) => r.Status == 1;

            return dbContext.ItemCateDb.GetList();
        }

        public static ItemCate Insert(ItemCate param)
        {
            var dbContext = new DbContext();
            var id = dbContext.ItemCateDb.InsertReturnIdentity(param);
            return GetById(param.Id);
        }

        public static void AddCache(ItemCate param)
        {
            if (param == null)
            {
                return;
            }

            if (GetItem(param.CateId) == null)
            {
                mDict.Add(param);
            }
        }

        public static ItemCate Update(ItemCate param)
        {
            var dbContext = new DbContext();
            dbContext.ItemCateDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ItemCateDb.DeleteById(id);
        }

    }
}

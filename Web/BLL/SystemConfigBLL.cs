using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Common;
using Entities.Request;
using SqlSugar;
using System.Linq.Expressions;
using Entities.Model;

namespace BLL
{
    public class SystemConfigBLL : DbContext, IInits
    {
        private static Dictionary<string, SystemItem> mData = null;

        public static SystemConfig SystemConfig = new SystemConfig();
        public void Init()
        {
            var all = GetAll();

            if (all != null && all.Count > 0)
            {
                mData = new Dictionary<string, SystemItem>(all.Count);
                foreach (var item in all)
                {
                    mData[item.ConfigKey] = item;
                }

                // 校验配置,并设置值
                var properties = SystemConfig.GetType().GetProperties();
                if(properties == null || properties.Length == 0)
                {
                    return;
                }

                List<String> errorList = new List<String>();
                foreach (var p in properties)
                {
                    if (mData.ContainsKey(p.Name) == false)
                    {
                        errorList.Add($"SystemConfig中的{p.Name}值未配置.");
                        continue;
                    }

                    try
                    {
                        object v = Convert.ChangeType(mData[p.Name].ConfigValue, p.PropertyType);
                        p.SetValue(mData, v, null);
                    }
                    catch (Exception ex)
                    {
                        errorList.Add($"SystemConfig转换出错 configKey:{p.Name} ex:{ex.ToString()}");
                    }
                }
                if (errorList.Count > 0)
                {
                    Logger.WriteErrorLog(errorList.ToListString(';'));
                }
            }
            else
            {
                mData = new Dictionary<string, SystemItem>();
            }
        }
        public static (bool, SystemItem) GetByName(string key)
        {
            SystemItem result = null;
            bool isExist = false;
            if (mData.ContainsKey(key))
            {
                result = mData[key];
                isExist = true;
            }
            return (isExist, result);
        }

        public static List<SystemItem> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.SystemConfigDb.GetList();
        }

        public static SystemItem Insert(SystemItem param)
        {
            var (isExist, result) = GetByName(param.ConfigKey);
 
            if (isExist)
            {
                Update(param);
            }
            else
            {
                var dbContext = new DbContext();
                dbContext.SystemConfigDb.InsertReturnIdentity(param);
            }
            mData[param.ConfigKey] = param;
            return param;
        }

        public static SystemItem Update(SystemItem param)
        {
            var dbContext = new DbContext();
            dbContext.SystemConfigDb.Update(param);
            mData[param.ConfigKey] = param;
            return param;
        }
        public static bool Delete(SystemItem param)
        {
            var dbContext = new DbContext();
            return dbContext.SystemConfigDb.Delete(param);
        }
    }
}

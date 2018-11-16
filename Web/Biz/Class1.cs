using Entities.DB;
using System;
using System.Collections.Generic;

namespace Biz
{
    public class Module
    {
        public static List<DBModule> GetList()
        {
           return  DAL.DB.QueryList<DBModule>();
        }

        public static DBModule Insert(DBModule param)
        {
            return DAL.DB.Insert<DBModule>(param);
        }

    }
}

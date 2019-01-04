using DotNet.Common;
using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{

    public class DB
    {
        internal static string dbConnection = ConfigHelper.GetSection("DBConnection");

        static DB()
        {
            //Server=DESKTOP-9QO1QAD;Database=bestddd;user=sa;password=sa123;
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = dbConnection,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,//自动释放数据务，如何存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //初始化主键和自增列信息到ORM的方式 codefirst
            });
           //db.CodeFirst.InitTables(typeof(Module), typeof(ModuleSub), typeof(Article), typeof(User), typeof(Words), typeof(Etyma));
            //db.CodeFirst.InitTables(typeof(Words));
        }

        protected static SqlSugarClient GetDB()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = dbConnection,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,//自动释放数据务，如何存在事务，在事务结束后释放
            });

            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                string paramStr = String.Empty;
                if (pars != null)
                {
                    foreach (var item in pars)
                    {
                        paramStr += $"{item.ParameterName} -> {item.Value.ToString()}   |";
                    }
                }
               // var log = db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                Console.WriteLine($"{sql}  ||  {paramStr}");
            };

            return db;
        }

        public static List<T> QueryList<T>() where T : class, new()
        {
            var db = GetDB();
            return db.Queryable<T>().ToList();
        }

        /// <summary>
        /// //根据主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T QueryById<T>() where T : class, new()
        {
            var db = GetDB();
            return db.Queryable<T>().InSingle(1);
        }

        public static List<T> QueryList<T>(Func<T, Boolean> fun) where T : class, new()
        {
            var db = GetDB();
            return db.Queryable<T>().Where(w => fun(w)).ToList();
        }

        public static (List<T>, Int32) QueryPageList<T>(T t,Func<T, Boolean> fun) where T : Pages, new()
        {
            var db = GetDB();
            var total = 0;
            var getPage = db.Queryable<T>().Where(w => fun(w)).OrderBy(t.Order).ToPageList(t.PageIndex, t.PageSize, ref total);//根据分页查询
            return (getPage, total);
        }


        public static T Insert<T>(T t) where T : class, new()
        {
            var db = GetDB();
            var id = db.Insertable(t).ExecuteReturnEntity();
            return id;
        }

        public static Int32 Update<T>(T t) where T : class, new()
        {
            var db = GetDB();
            var id = db.Updateable(t).ExecuteCommand();
            return id;
        }

        public static Int32 UpdateColums<T>(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whFun) where T : class, new()
        {
            var db = GetDB();
            var id = db.Updateable<T>().UpdateColumns(columns).Where(whFun).ExecuteCommand();
            return id;
        }

        public static Int32 Delete<T>(T t) where T : class, new()
        {
            var db = GetDB();
            var id = db.Deleteable(t).ExecuteCommand();
            return id;
        }
    }
}

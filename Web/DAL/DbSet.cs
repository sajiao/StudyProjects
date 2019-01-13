using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
   public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {

        }
        //SimpleClient中的方法满足不了你，你可以扩展自已的方法
        public List<T> GetByIds(dynamic[] ids)
        {
            return Context.Queryable<T>().In(ids).ToList();
        }

        public Result<T> GetPages(T req, Expression<Func<T, Boolean>> fun, PageInfo pageInfo)
        {
           return DB.QueryPageList<T>(req, fun, pageInfo);
        }
    }
}

using Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = DB.dbConnection,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });
        }

        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作

        public DbSet<ModuleSub> ModuleSubDb { get { return new DbSet<ModuleSub>(Db); } }

        public DbSet<Module> ModuleDb { get { return new DbSet<Module>(Db); } }

        public DbSet<Article> ArticleDb { get { return new DbSet<Article>(Db); } }
    }
}

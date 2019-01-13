using DAL;
using Entities;
using Entities.Request;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNet.Common;

namespace BLL
{
    public class ArticleBLL : DbContext
    {
        public static Article GetById(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ArticleDb.GetById(id);
        }

        public static List<Article> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.ArticleDb.GetList();
        }

        public static Result<Article> QueryPageList(ReqArticle req)
        {
            var dbContext = new DbContext();
            Expression<Func<Article, bool>> fun = null;
            if (req.Title.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.Title, req.Title);
            }

            var result = dbContext.ArticleDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            return result;
        }

        public static Article Insert(Article param)
        {
            var dbContext = new DbContext();
            var id = dbContext.ArticleDb.InsertReturnIdentity(param);
            return GetById(id);
        }

        public static Article Update(Article param)
        {
            var dbContext = new DbContext();
            dbContext.ArticleDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.ArticleDb.DeleteById(id);
        }
    }
}

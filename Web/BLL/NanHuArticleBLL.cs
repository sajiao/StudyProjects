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
    public class NanHuArticleBLL : DbContext
    {
        public static NanHuArticle GetById(int id)
        {
            var dbContext = new DbContext(); 
            return dbContext.NanHuArticleDb.GetById(id);
        }

        public static List<NanHuArticle> GetAll()
        {
            var dbContext = new DbContext();
            return dbContext.NanHuArticleDb.GetList();
        }

        public static Result<NanHuArticle> QueryPageList(ReqNanHuArticle req)
        {
            var dbContext = new DbContext();
            Expression<Func<NanHuArticle, bool>> fun = null;
            if (req.Title.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.Title, req.Title);
            }

            var result = dbContext.NanHuArticleDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            return result;
        }

        public static NanHuArticle Insert(NanHuArticle param)
        {
           var dbContext = new DbContext();
           var id = dbContext.NanHuArticleDb.InsertReturnIdentity(param);
            return GetById(id);
        }

        public static NanHuArticle Update(NanHuArticle param)
        {
            var dbContext = new DbContext();
            dbContext.NanHuArticleDb.Update(param);
            return GetById(param.Id);
        }
        public static bool Delete(int id)
        {
            var dbContext = new DbContext();
            return dbContext.NanHuArticleDb.DeleteById(id);
        }
    }
}

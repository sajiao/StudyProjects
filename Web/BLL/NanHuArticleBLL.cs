using DAL;
using Entities;
using Entities.Request;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNet.Common;
using Entities.Model;

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
            Expression<Func<NanHuArticle, bool>> fun = r => true;
            if (req.Title.IsNotNullOrEmpty())
            {
                fun = (r) => SqlFunc.Contains(r.Title, req.Title);
                //fun.AndAlso(r => SqlFunc.Contains(r.Title, req.Title));
            }

            if (req.CategoryId > 0)
            {
                var prefix = fun.Compile();
                fun = (r) => prefix(r) && r.CategoryId == req.CategoryId;
                // fun.AndAlso(r => r.CategoryId == req.CategoryId);
            }

            var result = dbContext.NanHuArticleDb.GetPages(req.ConvertData(), fun, req.PageInfo);
            if (result.TotalCount > 0)
            {
                result.Results.ForEach(r =>
                {
                    r.CategoryName = ModuleSubBLL.GetById(r.CategoryId)?.Name;
                });
            }
            return result;
        }

        public static NanHuArticle Insert(NanHuArticle param)
        {
            int id;
            if (param.Id > 0)
            {
               return Update(param);
            }
            else
            {
                var dbContext = new DbContext();
                id = dbContext.NanHuArticleDb.InsertReturnIdentity(param);
            }
           
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

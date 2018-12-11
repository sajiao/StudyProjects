using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace WebAPI.Filters
{
    /// <summary>
    /// 项目参数校验处理器
    /// </summary>
    public class ModelIsValidFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var result = ResultStatus.ModelIsVailFail;
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        string errMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                        result.Msg += errMsg + " | ";
                    }
                }

               // LogUtil.Warn($"模型校验失败:req:{context.HttpContext.Request.Path} method:{context.HttpContext.Request.Method}  code:{result.Code} errMsg:{result.Msg}");
                context.Result = new JsonResult(result);
            }
        }
    }
}

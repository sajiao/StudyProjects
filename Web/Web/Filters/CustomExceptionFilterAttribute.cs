using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Filters
{
    /// <summary>
    /// 项目异常处理器
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception == null)
            {
                return;
            }

            //记录日志
            //LogUtil.Error(context.Exception.ToString());

            //如果已经处理了，则忽略
            if (context.ExceptionHandled)
            {
                return;
            }

            //处理异常
            var result = ResultStatus.DataError;
            result.Msg = $"{result.Msg} {context.Exception.Message}";

            //context.Exception = null;
            context.Result = new JsonResult(result);

            //标记已经处理了异常
            context.ExceptionHandled = true;
        }
    }
}

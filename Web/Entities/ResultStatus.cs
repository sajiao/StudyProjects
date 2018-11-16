using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class ResultStatus
    {
        public static ResponseResult Success = new ResponseResult(0, "成功");
        public static ResponseResult DataError = new ResponseResult(1, "服务器错误");
        public static ResponseResult ModelIsVailFail = new ResponseResult(10001, "参数校验失败->");
    }
}

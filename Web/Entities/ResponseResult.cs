using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// 操作结果对象
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">操作描述信息</param>
        public ResponseResult(int code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">操作描述信息</param>
        /// <param name="result">返回结果</param>
        public ResponseResult(int code, string msg, object result)
        {
            this.Code = code;
            this.Msg = msg;
            this.Result = result;
        }
    }
}

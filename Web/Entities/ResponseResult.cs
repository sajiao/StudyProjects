using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// 操作结果对象
    /// </summary>
    public struct ResponseResult
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
        /// 构造函数
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">操作描述信息</param>
        public ResponseResult(int code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }
    }
}

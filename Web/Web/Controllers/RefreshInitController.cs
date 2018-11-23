using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// 刷新模块接口
    /// </summary>
    [Route("api/refreshinit")]
    public class RefreshInitController : BaseController
    {
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>操作结果</returns>
        [HttpGet("{name}")]
        public ResponseResult Get(string name)
        {
            InitBLL.Init();
            return ResultStatus.Success;
        }
    }
}
using Entities;
using Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using Entities.Model;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/systemconfig")]
    public class SystemConfigController : BaseController
    {
        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpGet]
        public ResponseResult Get()
        {
            return new ResponseResult(0, "", BLL.SystemConfigBLL.GetAll());
        }

        /// <summary>
        /// Post模块接口
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>操作结果</returns>
        // Put api/values
        [HttpPut]
        public SystemItem Put([FromBody]SystemItem value)
        {
            return BLL.SystemConfigBLL.Update(value);
        }
    }
}
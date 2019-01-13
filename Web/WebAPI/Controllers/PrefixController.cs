using Entities;
using Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/prefix")]
    public class PrefixController : BaseController
    {
        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpGet]
        public ResponseResult Get([FromQuery]ReqPrefix req)
        {
            req.PageInfo = HttpContext.Request.GetPageInfo();
            return new ResponseResult(0, "", BLL.PrefixBLL.QueryPageList(req));
        }

        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>操作结果</returns>
        // GET api/prefix/5
        [HttpGet("{id}")]
        public ResponseResult Get(int id)
        {
            return new ResponseResult(0, "", BLL.PrefixBLL.GetById(id));
        }

        /// <summary>
        /// Post模块接口
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>操作结果</returns>
        // POST api/prefix
        [HttpPost]
        public Prefix Post([FromBody]Prefix value)
        {
            return BLL.PrefixBLL.Insert(value);
        }

        /// <summary>
        /// Put模块接口
        /// </summary>
        /// <param name="value">value</param>
        [HttpPut]
        public Prefix Put([FromBody] Prefix value)
        {
            return BLL.PrefixBLL.Insert(value);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
using Entities;
using Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using Entities.Model;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/nanhuarticle")]
    public class NanHuArticleController : BaseController
    {
        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpGet]
        public ResponseResult Get([FromQuery]ReqNanHuArticle req)
        {
            req.PageInfo = HttpContext.Request.GetPageInfo();
            return new ResponseResult(0, "", BLL.NanHuArticleBLL.QueryPageList(req));
        }

        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>操作结果</returns>
        // GET api/NanHuArticle/5
        [HttpGet("{id}")]
        public ResponseResult Get(int id)
        {
            return new ResponseResult(0, "", BLL.NanHuArticleBLL.GetById(id));
        }

        /// <summary>
        /// Post模块接口
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>操作结果</returns>
        // POST api/values
        [HttpPost]
        public NanHuArticle Post([FromBody]NanHuArticle value)
        {
            return BLL.NanHuArticleBLL.Insert(value);
        }

        /// <summary>
        /// Put模块接口
        /// </summary>
        /// <param name="value">value</param>
        [HttpPut]
        public NanHuArticle Put([FromBody] NanHuArticle value)
        {
            return BLL.NanHuArticleBLL.Insert(value);
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Common;
using Entities;
using Entities.Model;
using Entities.Request;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Items")]
    public class ItemsController : BaseController
    {
        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpGet]
        public ResponseResult Get([FromQuery]ReqItems req)
        {
            req.PageInfo = HttpContext.Request.GetPageInfo();
            return new ResponseResult(0, "", BLL.ItemsBLL.QueryPageList(req));
        }

        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>操作结果</returns>
        // GET api/Items/5
        [HttpGet("{id}")]
        public ResponseResult Get(int id)
        {
            return new ResponseResult(0, "", BLL.ItemsBLL.GetById(id));
        }

        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>操作结果</returns>
        // GET api/Items/refresh/{key}
        [HttpGet("refresh/{key}")]
        public ResponseResult Refresh(string key)
        {
            if (key == "AutoReInit" + DateTime.Now.Hour.ToString())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                BLL.ItemsBLL.Refresh();
                sw.Stop();
                Logger.WriteProcessLog($"refresh key{key},耗时：{sw.ElapsedMilliseconds/1000} s, end time:{DateTime.Now}");
            }
          
            return new ResponseResult(0, "", "");
        }

        /// <summary>
        /// Post模块接口
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>操作结果</returns>
        // POST api/values
        [HttpPost]
        public Items Post([FromBody]Items value)
        {
            return BLL.ItemsBLL.Insert(value);
        }

        /// <summary>
        /// Put模块接口
        /// </summary>
        /// <param name="value">value</param>
        [HttpPut]
        public Items Put([FromBody] Items value)
        {
            return BLL.ItemsBLL.Insert(value);
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

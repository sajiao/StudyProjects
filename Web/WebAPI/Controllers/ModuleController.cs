﻿using Entities;
using Microsoft.AspNetCore.Mvc;
using Entities.Model;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 模块接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/module")]
    public class ModuleController : BaseController
    {
        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpGet]
        public ResponseResult Get()
        {
            return new ResponseResult(0,"", BLL.ModuleBLL.GetAll()) ;
        }

        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>操作结果</returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ResponseResult Get(int id)
        {
            return new ResponseResult(0, "", BLL.ModuleBLL.GetById(id));
        }

        /// <summary>
        /// Post模块接口
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>操作结果</returns>
        // POST api/values
        [HttpPost]
        public Module Post([FromBody]Module value)
        {
            return BLL.ModuleBLL.Insert(value);
        }

        /// <summary>
        /// Put模块接口
        /// </summary>
        /// <param name="value">value</param>
        [HttpPut]
        public Module Put([FromBody] Module value)
        {
            return BLL.ModuleBLL.Update(value);
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

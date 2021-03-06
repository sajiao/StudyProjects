﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Model;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/word")]
    public class WordController : BaseController
    {
        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpGet("{id}")]
        public ResponseResult Get(int id)
        {
            return new ResponseResult(0, "", BLL.WordsBLL.GetById(id));
        }

        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>操作结果</returns>
        [HttpGet]
        public ResponseResult Get([FromQuery]Words req)
        {
            return new ResponseResult(0, "", BLL.WordsBLL.GetByEtyma(req.EtymaId));
        }

        /// <summary>
        /// Post模块接口
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>操作结果</returns>
        // POST api/values
        [HttpPost]
        public Words Post([FromBody]Words value)
        {
            return BLL.WordsBLL.Insert(value);
        }

        /// <summary>
        /// Put模块接口
        /// </summary>
        /// <param name="value">value</param>
        [HttpPut("{id}")]
        public void Put([FromBody] Words value)
        {
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
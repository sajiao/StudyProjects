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
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshInitController : BaseController
    {
        // GET api/values/5
        [HttpGet("{name}")]
        public ResponseResult Get(string name)
        {
            InitBLL.Init();
            return ResultStatus.Success;
        }
    }
}
using Entities;
using Microsoft.AspNetCore.Mvc;
using Entities.Model;
using Entities.Request;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 模块接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/modulesub")]
    public class ModuleSubController : BaseController
    {
        /// <summary>
        /// 获取模块接口
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpGet]
        public ResponseResult Get([FromQuery]ReqModuleSub req)
        {
            return new ResponseResult(0, "", BLL.ModuleSubBLL.GetByModuleId(req.ModuleId));
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
            return new ResponseResult(0, "", BLL.ModuleSubBLL.GetById(id));
        }

        /// <summary>
        /// Post模块接口
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>操作结果</returns>
        // POST api/values
        [HttpPost]
        public ModuleSub Post([FromBody]ModuleSub value)
        {
            return BLL.ModuleSubBLL.Insert(value);
        }

        /// <summary>
        /// Put模块接口
        /// </summary>
        /// <param name="value">value</param>
        [HttpPut]
        public ModuleSub Put([FromBody] ModuleSub value)
        {
            return BLL.ModuleSubBLL.Update(value);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        [HttpDelete("{id}")]
        public ResponseResult Delete(int id)
        {
           var result = BLL.ModuleSubBLL.Delete(id);
           return  new ResponseResult(0, "", result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DB;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : BaseController
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<DBModule>> Get()
        {
            return BLL.ModuleBLL.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public DBModule Post([FromBody]DBModule value)
        {
           return BLL.ModuleBLL.Insert(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

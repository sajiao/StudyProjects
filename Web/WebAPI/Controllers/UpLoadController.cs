using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json", "multipart/form-data")]//此处为新增
    [ApiController]
    public class UpLoadController : BaseController
    {
        [HttpPost]
        [HttpPost("upload")]
        public ResponseResult upload(IFormFile file, string userId)
        {
            if (file != null)
            {
                var fileDir = AppContext.BaseDirectory;
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                //文件名称
                string projectFileName = file.FileName;

                //上传的文件的路径
                string filePath = fileDir + $@"\{projectFileName}";
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                return new ResponseResult(0, "ok");
            }
            else
            {
                return new ResponseResult(0, "no");
            }
        }

    }
}
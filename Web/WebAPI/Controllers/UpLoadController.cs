using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Common;
using DotNet.Common.Helper;
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

        [HttpGet]
        public ResponseResult Get()
        {
           
            var fileDir = Path.Combine(AppContext.BaseDirectory,"Upload");
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            //文件名称
            string projectFileName = "词霸天下38000词汇大全集 第1部分.pdf";

            //上传的文件的路径
            string filePath = fileDir + $@"\{projectFileName}";
            List<string> content = null;
            if (FileHelper.IsExistFile(filePath))
            {
               content =   PDFHelper.ReadPDFLine(filePath);
            }

            FilterEtyma(content);
            return new ResponseResult(0,"", content);
           
        }

        private List<string> FilterEtyma(List<string> contents)
        {
            List<string> filters = new List<string> { "Evaluation Warning : The document was created with Spire.PDF for .NET", "宋老师终极词汇完美教程", "宋老师超越母语者完美版", "终极词汇速记", "组词根综合", "课后问答联系方式", "宋老师口语素材" , "高考高分高能句型全总结", "宋老师英语词汇速记教材", "神奇后缀篇", "宋老师独家高中", "神奇前缀", "宋老师母语级词汇完美教程", "宋老师词霸天下系列", "母语级词汇速记", "词根统计100", "累积解析词汇量" };

            for (var i = contents.Count - 1; i >= 0; i--)
            {
                if (filters.Any(f => contents[i].Contains(f)))
                {
                    contents.RemoveAt(i);
                }
            }

            return contents;
        }

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
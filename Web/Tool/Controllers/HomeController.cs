using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Tool.Models;

namespace Tool.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> FileSave(List<IFormFile> files)
        {
            var data = Request.Form.Files;
            long size = data.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            List<string> filesName = null;
            foreach (var formFile in data)
            {
                filesName = new List<string>(data.Count);
                if (formFile.Length > 0)
                {
                    string fileExt = formFile.FileName; //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                    filesName.Add(newFileName);
                    var filePath = webRootPath + "/upload/" + newFileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            if (filesName != null)
            {
              DoTask(filesName);
            }

            //return File(stream, memi, Path.GetFileName(addrUrl));
            return Ok(new { Count = files.Count, Size= size, FileNames = filesName });
        }

        public async void DoTask(List<string> filesName)
        {
            if (filesName == null)
            {
                return;
            }
            string webRootPath = _hostingEnvironment.WebRootPath;
            DataTable dtDetail = null;
            DataTable dtMapping = null;
            foreach (var name in filesName)
            {
                var filePath = webRootPath + "/upload/" + name;
                var dt = OfficeHelper.ReadExcelToDataTable(filePath);
               
            }
        }
    }
}

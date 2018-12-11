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
using DotNet.Common;
using System.Text;

namespace Tool.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private static string uploadFolder = "";
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
            List<string> filesName = new List<string>();
            uploadFolder = webRootPath + "\\upload\\";
            foreach (var formFile in data)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = formFile.FileName; //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                    filesName.Add(newFileName);
                    var filePath = uploadFolder + newFileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            var zipName = "";
            if (filesName != null)
            {
              zipName =  DoTask(filesName);
            }

            //return File(stream, memi, Path.GetFileName(addrUrl));

            return Ok(new { Count = files.Count, Size= size, FileNames = filesName, downloadurl = "http://xl.bestddd.com/upload/"+zipName});
        }

        public string DoTask(List<string> filesName)
        {
            string zipFileName = "";
            try
            {
                if (filesName == null)
                {
                    return "";
                }

                DataTable dtDetail = null;
                DataTable dtMapping = null;

                List<AmountForMapping> mapping = null;

                List<MappingDetail> mappingDetail = null;

                foreach (var name in filesName)
                {
                    var filePath = uploadFolder + name;
                    var dt = OfficeHelper.ReadExcelToDataTable(filePath);
                    if (dt.Rows[0] == null)
                    {
                        break;
                    }

                    if (dt.Columns[0].ToString().Contains("Cash inDate"))
                    {
                        dtMapping = dt;
                        mapping = new List<AmountForMapping>(dtMapping.Rows.Count);
                        
                        for (int i = 0; i < dtMapping.Rows.Count; i++)
                        {
                            var row = dtMapping.Rows[i];
                            if (row["WBS"].ToString().TryTrim().IsNullOrEmpty())
                            {
                                continue;
                            }

                            var temp = new AmountForMapping()
                            {
                                Id = row["Id"].ToString().TryToInt(),
                                CashInDate = row["Cash inDate"].ToString().TryTrim(),
                                TotalAmount = row["Total Amount"].ToString().TryToDecimal(),
                                ByBatchAmount = row["By BatchAmount"].ToString().TryToDecimal(),
                                SapCo = row["SAP Co"].ToString().TryTrim(),
                                Term = row["Term"].ToString().TryTrim(),
                                WBS = row["WBS"].ToString().TryTrim(),
                            };
                            mapping.Add(temp);
                        }
                    }
                    else
                    {
                        dtDetail = dt;
                        mappingDetail = new List<MappingDetail>(dtDetail.Rows.Count);
                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            var row = dtDetail.Rows[i];
                            var temp = new MappingDetail()
                            {
                                Id = row["Id"].ToString().TryToInt(),
                                SAPCo = row["SAP Co"].ToString(),
                                DocumentValue = row["Document Value"].ToString().TryToDecimal(),
                                Terms = row["Terms"].ToString(),
                                WBSCode = row["WBS Code"].ToString(),

                            };
                            mappingDetail.Add(temp);
                        }
                    }
                }

                if (mapping == null || mappingDetail == null)
                {
                    return "";
                }
                Dictionary<AmountForMapping, List<MappingDetail>> result = new Dictionary<AmountForMapping, List<MappingDetail>>();

                foreach (var item in mapping)
                {
                    result[item] = Cal(item, mappingDetail);
                }

                zipFileName = ExportExcel(result, dtDetail);
            }
            catch (Exception e)
            {

                Logger.WriteErrorLog(e.Message);
            }

            return zipFileName;
        }

        private static List<MappingDetail> Cal(AmountForMapping item, List<MappingDetail> details)
        {
            
            List<MappingDetail> result = new List<MappingDetail>();
 
            try
            {
                 result = details.Where(d => d.WBSCode.Trim().StartsWith(item.WBS) && d.Terms.Trim().EqualsCurrentCultureIgnoreCase(item.Term) && d.SAPCo.IsEqual(item.SapCo.Split(','))).OrderBy(d => d.DocumentValue).ToList();

                var tempTotal = result.Sum(d => d.DocumentValue);
                var subTotal = tempTotal -item.TotalAmount;
                if (subTotal <= 0)
                {
                    return result;
                }
                result = CalAmount(item.TotalAmount, result,10m);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            return result;
        }


        public static List<MappingDetail> CalAmount(decimal inputAmount, List<MappingDetail> args,  decimal floatAmount)
        {

            Dictionary<decimal, List<MappingDetail>> result = new Dictionary<decimal, List<MappingDetail>>();
            List<MappingDetail> List2 = new List<MappingDetail>(args);
            for (int i = 0; i < List2.Count; i++)
            {
                List<MappingDetail> List3 = new List<MappingDetail>();
                List3.Add(args[i]);
                List2.RemoveAt(i);

                for (int j = 0; j < List2.Count; j++)
                {
                    if (List2[j].DocumentValue <= (inputAmount - Count(List3)))
                    {
                        List3.Add(List2[j]);
                        //List2.RemoveAt(j);
                        //j = -1;
                    }
                }
                result[Count(List3)] = List3;
                i = -1;
            }
            var tempResult = result.Where(r => (inputAmount - r.Key) <= floatAmount).OrderByDescending(r => r.Key);
            if (tempResult.Count() == 0)
            {
                tempResult = result.OrderByDescending(r => r.Key);
            }
            return tempResult.First().Value;
           
        }

        public static decimal Count(List<MappingDetail> list)
        {
            return list.Sum(a => a.DocumentValue);
        }
  
        private static string ExportExcel(Dictionary<AmountForMapping, List<MappingDetail>> dicts, DataTable dtDetail)
        {
            string zipFileName = "";
            try
            {
                List<string> filePaths = new List<string>();
                foreach (var item in dicts)
                {
                    DataTable newDt = dtDetail.Clone();

                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                    {
                        var row = dtDetail.Rows[i];

                        var isExist = item.Value.Any(d => d.Id == row.GetValue("Id").TryToInt());

                        if (isExist)
                        {
                            row["Group"] = item.Key.Id;
                            row["IsUsed"] = 1;
                            var newRow = newDt.NewRow();
                            newRow.ItemArray = row.ItemArray;
                            newDt.Rows.Add(newRow);
                        }
                    }

                    // newDt.Columns.Remove("Id");
                    string fileContent = newDt.GetCSVFormatData();

                    string filePath = Path.Combine(uploadFolder, item.Key.WBS + item.Key.SapCo + item.Key.Term + DateTime.Now.ToString("yyyyMMddHHmmsssss") + ".csv");

                    FileHelper.CreateFile(filePath, fileContent, Encoding.UTF8);
                    filePaths.Add(filePath);
                }

                string detailMapPath = Path.Combine(uploadFolder, "detailMap"+ DateTime.Now.ToString("yyyyMMddHHmmsssss") + ".csv");
                string detailContent = dtDetail.GetCSVFormatData();
                FileHelper.CreateFile(detailMapPath, detailContent, Encoding.UTF8);
                filePaths.Add(detailMapPath);
                zipFileName = "DetailFiles" + DateTime.Now.ToString("yyyyMMddHHmmsssss") + ".zip";
                ZipHelper.CompressFile(filePaths,null, Path.Combine(uploadFolder, zipFileName));
          
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.Message);
            }

            return zipFileName;
        }
    }
}

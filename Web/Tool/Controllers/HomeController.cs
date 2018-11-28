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
            uploadFolder = webRootPath + "/upload/";
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

            if (filesName != null)
            {
              DoTask(filesName);
            }

            //return File(stream, memi, Path.GetFileName(addrUrl));
            return Ok(new { Count = files.Count, Size= size, FileNames = filesName });
        }

        public async void DoTask(List<string> filesName)
        {
            try
            {
                if (filesName == null)
                {
                    return;
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
                    return;
                }
                Dictionary<AmountForMapping, List<MappingDetail>> result = new Dictionary<AmountForMapping, List<MappingDetail>>();

                foreach (var item in mapping)
                {
                    result[item] = Cal(item, mappingDetail);
                }

                ExportExcel(result, dtDetail);
            }
            catch (Exception e)
            {

                Logger.WriteErrorLog(e.Message);
            }
            
        }

        private static List<MappingDetail> Cal(AmountForMapping item, List<MappingDetail> details)
        {
            
            List<MappingDetail> result = new List<MappingDetail>();
 
            try
            {
                 result = details.Where(d => d.WBSCode.Trim().EqualsCurrentCultureIgnoreCase(item.WBS) && d.Terms.Trim().EqualsCurrentCultureIgnoreCase(item.Term) && d.SAPCo.IsEqual(item.SapCo.Split(','))).OrderBy(d => d.DocumentValue).ToList();
                var tempTotal = result.Sum(d => d.DocumentValue);
                var subTotal = tempTotal -item.TotalAmount;
                if (subTotal <= 0)
                {
                    return result;
                }
                result = Map(item.TotalAmount, result);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            return result;
        }

        private static List<MappingDetail> Map(decimal amount, List<MappingDetail> items)
        {
            decimal floatAmount = 10m;
            try
            {
                decimal tempAmount = 0;
                List<MappingDetail> matchList = new List<MappingDetail>(items.Count);
                if (items.Count == 0) return matchList;
                List<int> removeListIndex = new List<int>();
                decimal subAmount = 0m;
              
                for (int i = 0; i < items.Count; i++)
                {
                    subAmount = amount - tempAmount;
                    var item = items[i];

                    if (subAmount < item.DocumentValue)
                    {
                        break;
                    }
                    tempAmount += item.DocumentValue;
                    matchList.Add(item);
              
                }

                if (subAmount >= -floatAmount && subAmount <= 0)
                {
                    return matchList;
                }
                else
                {
                    return reCal(amount, items, matchList,0,0);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
               
            }

            return new List<MappingDetail>();
        }

        private static List<MappingDetail> reCal(decimal amount,List<MappingDetail> items, List<MappingDetail> matchList, int num, int restNum)
        {
            //已经匹配的总金额
            var origialMathTotalAmount = matchList.Sum(m => m.DocumentValue);
            //剩下的差额
            var subAmount = amount - origialMathTotalAmount;
            //已经匹配的最大index
            var maxMatchIndex = matchList.Count == 0 ? 0 : matchList.Count - 1 - num;
            for (int i = 0; i < maxMatchIndex; i++)
            {
                var matchIndex = maxMatchIndex - i;
                var matchItem = matchList.Count == 0 ? null : matchList[matchIndex];
                var matchLastAmount = matchList.Count == 0 ? 0 : matchItem.DocumentValue;
                var index = matchList.Count + 1 + restNum;

                if (items.Count <= index) break;

                var restItem = items[index];
                var tempTotalAmount = origialMathTotalAmount - matchLastAmount;
                var newAmount = tempTotalAmount + restItem.DocumentValue;
                var tempNewSubAmount = newAmount - amount;
                var newMathList = matchList.Where(m=> m.DocumentValue <= tempNewSubAmount && m.DocumentValue >= (tempNewSubAmount -10));

                if (newMathList.Count() == 0)
                {
                    if (maxMatchIndex < num)
                    {
                       return reCal(amount, items, matchList, 0, restNum + 1);
                    }
                    else if ((items.Count - matchList.Count) <= restNum)
                    {
                        break;
                    }

                    return reCal(amount, items, matchList, num + 1, restNum);
                }

                var lastItem = newMathList.Last();
                matchList.RemoveAt(matchIndex);
                matchList.Remove(lastItem);
                matchList.Add(restItem);
                return matchList;
            }
            return matchList;
        }

        private static int GetRandom(int min, int max, List<int> removeList)
        {
            Random random = new Random();
            var index = random.Next(min, max);
            if (removeList.Any(r => r == index))
            {
               return GetRandom(min, max, removeList);
            }

            return index;
        }

        private static void ExportExcel(Dictionary<AmountForMapping, List<MappingDetail>> dicts, DataTable dtDetail)
        {
            try
            {
                foreach (var item in dicts)
                {
                    DataTable newDt = dtDetail.Clone();

                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                    {
                        var row = dtDetail.Rows[i];

                        var isExist = item.Value.Any(d => d.Id == row.GetValue("Id").TryToInt());

                        if (isExist)
                        {
                            var newRow = newDt.NewRow();
                            newRow.ItemArray = row.ItemArray;
                            newDt.Rows.Add(newRow);
                        }
                    }

                    // newDt.Columns.Remove("Id");
                    string fileContent = newDt.GetCSVFormatData();

                    string filePath = uploadFolder + item.Key.WBS + item.Key.SapCo + item.Key.Term + DateTime.Now.ToString("yyyyMMddHHmmsssss") + ".csv";

                    FileHelper.CreateFile(filePath, fileContent, Encoding.UTF8);
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.Message);
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL;
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
        public ResponseResult Get(int type)
        {
            if (type == 0) return new ResponseResult(0, "no");

            var fileDir = Path.Combine(AppContext.BaseDirectory,"Upload");
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            //文件名称
            string projectFileName = "38000词汇500词根全集.pdf";

            switch (type)
            { 
                case 1:
                    projectFileName = "38000词汇500词根全集.pdf";
                    break;
                case 2:
                    projectFileName = "词霸天下38000词汇大全集第3部分.pdf";
                    break;

            }
            
            //上传的文件的路径
            string filePath = fileDir + $@"\{projectFileName}";
            List<string> content = null;
            if (FileHelper.IsExistFile(filePath))
            {
               content =   PDFHelper.ReadPDFLine(filePath);
            }

            Filter(content);
            if (type == 1)
            {
                AddEtyma(content);
            }
            else if (type >= 2)
            {
                AddWords(content);
            }
           
            return new ResponseResult(0,"", content);
           
        }


        /// <summary>
        /// 词霸天下38000词汇大全集第1部分.pdf
        /// </summary>
        /// <param name="contents"></param>
        private void AddWords(List<string> contents)
        {
         
            if (contents.IsNullOrEmpty()) return;

            Regex reg = new Regex(@"[^0-9.][\x00-\xff]{1,}");
            Regex regZh = new Regex(@"[^\x00-\xff].*$");
            Regex regYB = new Regex(@"\[.*\]");
            Regex regWordFreq = new Regex(@"(【).*(】)");
            ///DBCC CHECKIDENT ('words', RESEED,31596) 
            //var lastIndex = contents.LastIndexOf("1. -vinc- = -vict-  胜，征服");
            //var lastIndex = contents.LastIndexOf("101. -sur-  确定");
            var lastIndex = contents.LastIndexOf("201. -honor- = -hono- = -honest-  荣誉");
            string wordSrouce = "【词源】";
            string wordExtion = "【引申】";
            string wordFreq = "【词频 ";
            contents.RemoveRange(0, lastIndex);
            Etyma etyma = new Etyma();
            bool isEtyma = false;
            string wordSrouceTemp = string.Empty;
            string wordExtionTemp = string.Empty;
            string wordDesc = string.Empty;
            int index = 0;
            Words entity = new Words();
            int totalIndex = contents.Count();
            foreach (var item in contents)
            {
                try
                {
                    
                    string etymaDesc = reg.Match(item).Value.Replace("-", "").TryTrim();
                    (bool isExist, var etymatemp) = EtymaBLL.GetByDesc(etymaDesc);
                    if (isExist)
                    {
                        etyma = etymatemp;
                        isEtyma = true;
                    }
                    if (isEtyma)
                    {
                        if (item.Contains(wordSrouce))
                        {
                            wordSrouceTemp += item.Replace(wordSrouce, "");
                        }
                        else if (item.Contains(wordExtion))
                        {
                            wordExtionTemp += item.Replace(wordExtion, ""); ;
                        }
                        else
                        {
                            if (wordExtionTemp.IsNullOrEmpty() && wordSrouceTemp.IsNotNullOrEmpty())
                            {
                                wordSrouceTemp += item;
                            }
                        }
                        bool isSave = false;
                        if (totalIndex == (index + 1))
                        {
                            isSave = true;
                        }
                        else
                        {
                            var nextItem = contents[index + 1];
                            if (nextItem.Contains(wordFreq))
                            {
                                isSave = true;
                            }
                        }
                        
                        if (isSave)
                        {
                            isEtyma = false;
                            etyma.Extention = wordExtionTemp;
                            etyma.EtymaSource = wordSrouceTemp;
                            EtymaBLL.Update(etyma);
                        }
                    }
                    else
                    {
                        bool isSave = false;
                        if (totalIndex == (index + 1))
                        {
                            isSave = true;
                        }
                        else
                        {
                            var nextItem = contents[index + 1];
                            isSave = regYB.Match(nextItem).Success;
                        }

                        if (wordDesc.IsNullOrEmpty())
                        {
                            wordDesc = item;
                        }

                        if (isSave)
                        {
                            var matches = regWordFreq.Matches(wordDesc);

                            if (matches.HasValue())
                            {
                                var matchValue = matches.First().Value.IndexOf(wordFreq) > 0 ? matches.First().Value.Remove(0, matches.First().Value.IndexOf(wordFreq)) : matches.First().Value;
                                var tempStr = matchValue.Replace(wordFreq, "").TryTrim();
                                var freq = tempStr.Substring(0, tempStr.IndexOf("】")).TryTrim();
                                var arr = freq.Split("，");
                                entity.Frequency = arr[0].TryToInt();
                                if (arr.Length > 1)
                                {
                                    entity.Frequency2 = freq.Replace(arr[0] + "，", "");
                                }

                                wordDesc = wordDesc.Replace(matchValue, "");
                            }

                            entity.EtymaId = etyma.Id;
                            entity.FullDesc = wordDesc;
                            entity.Desc = wordDesc.TryTrim();
                            entity.ZhDesc = regZh.Matches(wordDesc).Last().Value.TryTrim();
                            if (entity.ZhDesc.Contains("]"))
                            {
                                entity.ZhDesc = entity.ZhDesc.Remove(0, entity.ZhDesc.IndexOf("]") + 1).TryTrim();
                            }
                            entity.PhoneticSymbolUK = regYB.Match(wordDesc).Value;
                            entity.Word = wordDesc.Substring(0, wordDesc.IndexOf(entity.PhoneticSymbolUK)).TryTrim();
                            entity.Status = 1;

                            WordsBLL.Insert(entity);
                            wordDesc = string.Empty;
                            entity = new Words();
                        }
                        else
                        {
                            if (wordDesc != item)
                            {
                                wordDesc += item;
                            }

                        }
                    }

                    index++;
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(string.Format("WordDesc:{2},Error Message:{0},StackTrace:{1}", ex.Message, ex.StackTrace, item));
                    throw ex;
                }
            }
           
            
        }

        /// <summary>
        /// 38000词汇500词根全集.pdf
        /// </summary>
        /// <param name="contents"></param>
        private void AddEtyma(List<string> contents)
        {
            if (contents.IsNullOrEmpty()) return;

            Regex reg = new Regex(@"[^0-9.][\x00-\xff]{1,}");
            Regex regZh = new Regex(@"[^\x00-\xff].*$");
            foreach (var item in contents)
            {
                Etyma entity = new Etyma();

                entity.FullDesc = item;
                entity.Desc = reg.Match(item).Value.Replace("-", "").TryTrim();
                entity.ZhDesc = regZh.Matches(item).Last().Value.TryTrim();
                entity.Word = entity.Desc.Split("=")[0].TryTrim();
                entity.EtymaSource = entity.Word;
                entity.Status = 1;
                EtymaBLL.Insert(entity);
            }
        }

        private List<string> Filter(List<string> contents)
        {
            List<string> filters = new List<string> { "Evaluation Warning : The document was created with Spire.PDF for .NET", "宋老师终极词汇完美教程", "宋老师超越母语者完美版", "终极词汇速记", "组词根综合", "课后问答联系方式", "宋老师口语素材", "高考高分高能句型全总结", "宋老师英语词汇速记教材", "神奇后缀篇", "宋老师独家高中", "神奇前缀", "宋老师母语级词汇完美教程", "宋老师词霸天下系列", "母语级词汇速记", "词根统计100", "累积解析词汇量", "13	/	13" };

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
        public ResponseResult Post(List<IFormFile> files)
        {
            if (files != null)
            {
                var file = files[0];
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
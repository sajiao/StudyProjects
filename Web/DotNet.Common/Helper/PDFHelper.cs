
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Spire.Pdf;

namespace DotNet.Common.Helper
{
    public class PDFHelper
    {
        public static string ReadPdfContent(string filepath)
        {
            try
            {
                //实例化一个PdfDocument对象
                PdfDocument doc = new PdfDocument();

                //加载PDF文档
                doc.LoadFromFile(filepath);

                //实例化一个StringBuilder 对象
                StringBuilder content = new StringBuilder(500);

                //提取PDF所有页面的文本
                foreach (PdfPageBase page in doc.Pages)
                {
                    content.Append(page.ExtractText());
                }

                var text = content.ToString();
                return text;
            }
            catch (Exception ex)
            {
               
                Logger.WriteErrorLog("出错文件：" + filepath + "原因：" + ex.ToString());
                return null;
            }
        }

        public static List<string> ReadPDFLine(string filepath)
        {
            string content = ReadPdfContent(filepath);
            var arr = content.Split('\n');
            List<string> result = new List<string>(arr.Length);
            foreach (var item in arr)
            {
                if (item.IsNullOrEmpty()) continue;

                var temp = item.Trim('\t').Trim();

                if (temp.IsNullOrEmpty()) continue;

                result.Add(temp.Trim('\r').Trim('\n'));
            }

            return result;
        }
    }
}

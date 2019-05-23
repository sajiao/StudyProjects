using Entities.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MyTools.Import
{
    public class WordImport
    {
        public static void Import(string path)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int num = 0;
            var files = Directory.GetFiles(path, "*.xml");
            var tempList = new List<Words>(500);
            foreach (var item in files)
            {
                
                XElement xe = XElement.Load(item);
                IEnumerable<XElement> elements = from ele in xe.Elements("item")
                                                 select ele;

                foreach (var ele in elements)
                {
                    num++;

                    var word = new Words();
                    word.Frequency = num;
                    word.Word = ele.Element("word").Value;
                    if(ele.Element("trans") != null)
                    {
                        word.Trans = ele.Element("trans").Value.Replace("\"", "");
                        var tic = ele.Element("phonetic").Value;
                        var temp = tic.Split("]");
                        word.PhoneticSymbolUS = temp[0].Trim()+"]";
                        if(temp.Length > 1)
                        {
                            word.PhoneticSymbolUK =  temp[1].Trim() +"]";
                        }
                    }
                    tempList.Add(word);

                    if (tempList.Count == 500)
                    {
                        BLL.WordsBLL.BatchInsert(tempList);
                        tempList.Clear();
                    }
                }

                if (tempList.Count > 0)
                {
                    BLL.WordsBLL.BatchInsert(tempList);
                    tempList.Clear();

                }
            }
            
            sw.Stop();
            Console.WriteLine("WordImport.Import done, total count:" + num.ToString()+"; 耗时："+ sw.ElapsedMilliseconds.ToString() +" ms");
        }

    }
}

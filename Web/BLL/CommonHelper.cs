using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Common;
using Entities.Model;

namespace BLL
{
   public static class CommonHelper
    {
        /// <summary>
        /// 拆分前缀后缀方法
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<FixExtension> GetWord(string json)
        {
            List<FixExtension> result = new List<FixExtension>();
            if (json.IsNotNullOrEmpty() && json.Contains('~'))
            {
                var arr1 = json.Split('~');
                foreach (var item1 in arr1)
                {
                    var newItem = new FixExtension();
                    var arr2 = item1.Split('$');
                    newItem.Meaning = arr2[0].TryTrim();
                    newItem.Words = new List<FixWord>();
                    var arr3 = arr2[1].Split('&');
                    foreach (var item3 in arr3)
                    {
                        if (item3.TryTrim().IsNotNullOrEmpty())
                        {
                            continue;
                        }

                        var word = new FixWord();
                        var arr4 = item3.Split('|');
                        word.Word = arr4[0].TryTrim();
                        word.PhoneticSymbolUK = arr4[1].TryTrim();
                        // word.PhoneticSymbolUS = arr4[2].TryTrim();
                        word.ZhDesc = arr4[2].TryTrim();
                        newItem.Words.Add(word);
                    }
                    result.Add(newItem);
                }
            }

            return result;
        }
    }
}

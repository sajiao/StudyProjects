using DotNet.Common;
using Entities;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.ThirtyPart
{
    public static class TaoBaoKeHelper
    {

        /// <summary>
        /// 没有关键词，导入前一万条
        /// </summary>
        public static void ImportTaobaoke()
        {
            var dt = DateTime.Now;
            Func<int, object> fun = (pageIndex) =>
            {
                try
                {
                    List<Items> updateItems = new List<Items>(200);
                    List<Items> addItems = new List<Items>(200);
                    var items = ItemsBLL.GetData();

                    var result = QueryCoupon(string.Empty, pageIndex);

                    if (result == null || result.Count() == 0)
                    {
                        return null;
                    }
                    foreach (var item2 in result)
                    {
                        var temp = items.FirstOrDefault(a => a.NumIid == item2.NumIid);
                        if (temp != null)
                        {
                            item2.Id = temp.Id;
                            temp = item2;
                            if (updateItems.Exists(d => d.NumIid == temp.NumIid))
                            {
                                updateItems.Add(temp);
                            }
                        }
                        else
                        {
                            items.Add(item2);
                            addItems.Add(item2);
                        }
                    }

                    Task.Factory.StartNew(() => {
                        if (addItems.Count > 0)
                        {
                            ItemsBLL.BatchInsert(addItems);
                        }
                        if (updateItems.Count > 0)
                        {
                            ItemsBLL.BatchUpdate(updateItems);
                        }
                    });

                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex.Message);
                }
                return null;
            };

            for (int i = 1; i <= 100; i++)
            {
                FuncTimeout.EventNeedRun action = delegate (object[] param)
                {
                    //调用自定义函数
                    return fun(param[0].ToString().TryToInt());
                };
                FuncTimeout ft = new FuncTimeout(action, 1000 * 60 * 1);//超时时间2分钟
                ft.doAction(i);
                Thread.Sleep(5000);
            }
            try
            {
                ItemsBLL.ClearSameData();

                Console.WriteLine("Done:" + DateTime.Now);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }

        }

        /// <summary>
        /// 按关键词导入，每个关键词只能查询100条
        /// </summary>
        public static void ImportAllTaobaoke()
        {
            var dt = DateTime.Now;
            PageInfo page = new PageInfo();
            page.PageSize = 100;
            var cateItems = ItemCateBLL.GetData();

            foreach (var item in cateItems)
            {
                if (item.LastUpdateTime > dt.AddDays(-10))
                {
                    continue;
                }

                Func<string,object> fun = (keyword) =>
                 {
                    try
                    {
                        List<Items> updateItems = new List<Items>(200);
                        List<Items> addItems = new List<Items>(200);
                        var items = ItemsBLL.GetByTag(keyword);
                        page.PageIndex = 1;
                        var result = QueryCoupon(item.CateName);

                        //Timeout timeout = new Timeout();
                        //timeout.Do = QueryCoupon(item.CateName);

                        if (result == null || result.Count() == 0)
                        {
                            return null;
                        }
                        foreach (var item2 in result)
                        {
                            var temp = items.FirstOrDefault(a => a.NumIid == item2.NumIid);
                            if (temp != null)
                            {
                                item2.Id = temp.Id;
                                temp = item2;
                                if (updateItems.Exists(d => d.NumIid == temp.NumIid))
                                {
                                    updateItems.Add(temp);
                                }
                            }
                            else
                            {
                                items.Add(item2);
                                addItems.Add(item2);
                            }
                        }

                        item.LastUpdateTime = dt;
                        Task.Factory.StartNew(() => {
                            if (addItems.Count > 0)
                            {
                                ItemsBLL.BatchInsert(addItems);
                            }
                            if (updateItems.Count > 0)
                            {
                                ItemsBLL.BatchUpdate(updateItems);
                            }
                            ItemCateBLL.Update(item);
                        });

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(ex.Message);
                    }
                    return null;
                };
                 FuncTimeout.EventNeedRun action = delegate (object[] param)
                 {
                    //调用自定义函数
                     return fun(param[0].ToString());
                 };
                FuncTimeout ft = new FuncTimeout(action, 1000 * 60 * 2);//超时时间2分钟
                ft.doAction(item.CateName);
                Thread.Sleep(5000);
            }
            try
            {

                //ItemCateBLL.BatchUpdate(cateItems);

                ItemsBLL.ClearSameData();

                Console.WriteLine("Done:" + DateTime.Now);

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }

            //TaskParallelHelper.ExecuteTask(actionItem);
        }

        public static List<Items> QueryCoupon(string keyword, int pageIndex = 1)
        {
            List<Items> result = new List<Items>(200);
            try
            {
                PageInfo page = new PageInfo();
                page.PageIndex = pageIndex;
                page.PageSize = 100; //api 最多返回100条数据
                result.AddRange(TaoBaoKe.QueryDgItemCoupon(page, 0, keyword, 1));
                var tianmaoresult = TaoBaoKe.QueryDgItemCoupon(page, 0, keyword, 2);
                if (result.Count == 0 && tianmaoresult.Count == 0)
                {
                    return result;
                }
                result.AddRange(tianmaoresult);
                TaoBaoKe.QueryProductDetail(result, 1);
                TaoBaoKe.QueryProductDetail(result, 2);

                Logger.WriteProcessLog($"keyword:{keyword},totalCount:{result.Count},pageIndex:{page.PageIndex}, pageSize:{page.PageSize},DateTime:{DateTime.Now}");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }

            return result;
        }
    }
}

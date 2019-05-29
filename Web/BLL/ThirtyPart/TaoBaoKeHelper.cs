using Entities;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ThirtyPart
{
    public static class TaoBaoKeHelper
    {
        public static List<Items> QueryCoupon(string keyword)
        {
            PageInfo page = new PageInfo();
            page.PageSize = 100;
            page.PageIndex = 1;
            var result = TaoBaoKe.QueryDgItemCoupon(page, keyword);
            if (result.Count == 0)
            {
                return result;
            }
            TaoBaoKe.QueryProductDetail(result, 1);
            TaoBaoKe.QueryProductDetail(result, 2);
            List<Items> updateItems = new List<Items>(50);
            foreach (var item in result)
            {
                if (ItemsBLL.GetItem(item.NumIid) != null)
                {
                    item.Status = 2;
                    updateItems.Add(item);
                }
                else
                {
                    ItemsBLL.AddCache(item);
                }
            }

            if (updateItems.Count > 0)
            {
                ItemsBLL.BatchUpdate(updateItems);
            }

            ItemsBLL.BatchInsert(result);
            ItemsBLL.UpdateCache(result);
            ItemsBLL.DeleteByStatus();
            return result;
        }
    }
}

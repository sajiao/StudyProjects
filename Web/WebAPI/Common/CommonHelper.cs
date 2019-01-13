using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Common
{
    public static class CommonHelper
    {
        public static PageInfo GetPageInfo(this HttpRequest httpReq)
        {
            var s = httpReq.Query["pageIndex"];
            var s2 = httpReq.Query["pageSize"];
            var sortFields = httpReq.Query["sortFields"];
            var sort = httpReq.Query["sort"];
            PageInfo pageInfo = new PageInfo();
            int defaultPageIndex = 0;
            pageInfo.PageIndex = defaultPageIndex;
            if (int.TryParse(s, out defaultPageIndex))
            {
                pageInfo.PageIndex = defaultPageIndex;
            }
            int defaultPageSize = 10;
            pageInfo.PageSize = defaultPageSize;
            if (int.TryParse(s2, out defaultPageSize))
            {
                pageInfo.PageSize = defaultPageSize;
            }
            pageInfo.SortFields = sortFields;
            pageInfo.Sort = sort;
            return pageInfo;
        }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class Base
    {
        [SugarColumn(ColumnDescription = "是否删除")]
        public bool IsDelete { get; set; }

        [SugarColumn(ColumnDescription = "创建时间")]
        public int CreateTime { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public int LastUpdateTime { get; set; }
    }
}

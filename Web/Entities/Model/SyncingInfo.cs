using SqlSugar;
using System;

namespace Entities.Model
{
    [SugarTable("SyncingInfo", "同步信息")]
   public class SyncingInfo
    {
        [SugarColumn(Length = 255, IsNullable = false, ColumnDescription = "同步唯一标识(数据库表名)")]
        public string Identifier { get; set; }

        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "正在同步的文件路径")]
        public string FilePath { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "文件偏移量")]
        public long FileOffset { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "最后一次更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}

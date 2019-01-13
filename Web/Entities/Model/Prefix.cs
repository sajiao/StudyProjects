using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("Prefix", "前缀表")]
   public class Prefix
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "单词")]
        public string Word { get; set; }

        [SugarColumn(Length = 8000, IsNullable = false, ColumnDataType = "Nvarchar", ColumnDescription = "描述")]
        public string Desc { get; set; }

        [SugarColumn(Length = 10000, IsNullable = false, ColumnDescription = "词根来源")]
        public string Source { get; set; }

        [SugarColumn(Length = 10000, IsNullable = false, ColumnDescription = "Json")]
        public string Json { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "状态:1 正常")]
        public int Status { get; set; }

        [SugarColumn(ColumnDescription = "创建者")]
        public Guid CreatedBy { get; set; }

        [SugarColumn(ColumnDescription = "创建时间")]
        public int CreatedTime { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public int LastUpdatedTime { get; set; }

        [SugarColumn(IsIgnore = true, ColumnDescription = "Extensions")]
        public List<FixExtension> Extensions { get; set; }
    }

}

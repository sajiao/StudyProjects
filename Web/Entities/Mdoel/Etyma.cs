using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("Etyma", "词根表")]
    public class Etyma
    {
        [SugarColumn(Length = 32, IsNullable = false, IsPrimaryKey = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "单词")]
        public string Word { get; set; }


        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "词根描述")]
        public string Desc { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "中文意思")]
        public string ZhDesc { get; set; }

        [SugarColumn(Length = 10000, IsNullable = false, ColumnDescription = "词根来源")]
        public string EtymaSource { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "状态:1 正常")]
        public int Status { get; set; }

        [SugarColumn(ColumnDescription = "创建者")]
        public Guid CreatedBy { get; set; }

        [SugarColumn(ColumnDescription = "创建时间")]
        public int CreatedTime { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public int LastLoginTime { get; set; }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("Words", "单词表")]
    public class Words
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "单词")]
        public string Word { get; set; }

        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "单词拆分")]
        public string SplitWord { get; set; }

        [SugarColumn(Length = 400, IsNullable = true, ColumnDescription = "单词拆分描述")]
        public string SplitWordDesc { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "单词词源关联ID")]
        public int EtymaId { get; set; }

        [SugarColumn(IsIgnore = true, ColumnDescription = "Etyma Word")]
        public string EtymaWord { get; set; }

        [SugarColumn(Length = 8000, IsNullable = false, ColumnDataType = "Nvarchar", ColumnDescription = "描述")]
        public string Desc { get; set; }

        [SugarColumn(Length = 200, IsNullable = true, ColumnDataType = "Nvarchar", ColumnDescription = "英式音标")]
        public string PhoneticSymbolUK { get; set; }

        [SugarColumn(Length = 400, IsNullable = true, ColumnDescription = "英式音标读音地址")]
        public string PhoneticSymbolUKUrl { get; set; }

        [SugarColumn(Length = 200, IsNullable = true, ColumnDataType = "Nvarchar", ColumnDescription = "美式音标")]
        public string PhoneticSymbolUS { get; set; }

        [SugarColumn(Length = 400, IsNullable = true, ColumnDescription = "美式音标读音地址")]
        public string PhoneticSymbolUSUrl { get; set; }

        [SugarColumn(Length = 8000, IsNullable = false, ColumnDataType = "Nvarchar", ColumnDescription = "词根描述")]
        public string FullDesc { get; set; }

        [SugarColumn(Length = 5000, IsNullable = false, ColumnDescription = "中文意思")]
        public string ZhDesc { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "词频")]
        public int Frequency { get; set; }

        [SugarColumn(Length = 100, IsNullable = true, ColumnDescription = "词频")]
        public string Frequency2 { get; set; }

        [SugarColumn(Length = 100, IsNullable = true, ColumnDescription = "考纲")]
        public string Examination { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "类别")]
        public int CategoryId { get; set; }

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

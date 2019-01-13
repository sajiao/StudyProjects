using System;
using System.Collections.Generic;
using SqlSugar;

namespace Entities
{
    [SugarTable("Module", "模块表")]
    public class Module
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 30, IsNullable = false, ColumnDescription = "别名")]
        public string Alias { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "名称")]
        public string Name { get; set; }

        [SugarColumn(ColumnDescription ="是否开放")]
        public bool IfOpen { get; set; }

        [SugarColumn(ColumnDescription = "父ID")]
        public int ParentId { get; set; }

        [SugarColumn(IsIgnore = true ,ColumnDescription = "test in")]
        public int TestId { get; set; }
    }
}

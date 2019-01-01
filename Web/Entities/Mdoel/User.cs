using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [SugarTable("User", "用户表")]
    public class User
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "类别ID")]
        public int TypeId { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "名称")]
        public string Name { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "昵称")]
        public string NickName { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "密码")]
        public string Password { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "状态:1 正常")]
        public int Status { get; set; }

        [SugarColumn(ColumnDescription = "创建时间")]
        public int CreatedTime { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public int LastLoginTime { get; set; }
    }
}

using SqlSugar;
using System;

namespace Entities
{
    [SugarTable("Article", "文章表")]
    public class Article
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "类别ID")]
        public int CategoryId { get; set; }

        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "文章名称")]
        public string Title { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "text", ColumnDescription = "文章内容")]
        public string Content { get; set; }

        [SugarColumn(Length = 400, IsNullable = false, ColumnDescription = "文章简介")]
        public string Summary { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "状态:1 正常")]
        public int Status { get; set; }

        [SugarColumn(ColumnDescription = "是否删除")]
        public bool IsDeleted { get; set; }

        [SugarColumn(ColumnDescription = "创建时间")]
        public int CreatedTime { get; set; }

        [SugarColumn(ColumnDescription = "创建者")]
        public Guid CreatedBy { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public int LastUpdatedTime { get; set; }

        [SugarColumn(ColumnDescription = "创建者")]
        public Guid LastUpdatedBy { get; set; }
    }
}

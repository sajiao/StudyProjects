using SqlSugar;
using System;

namespace Entities.Model
{
    [SugarTable("NanHuArticle", "南湖文章表")]
    public class NanHuArticle
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

        [SugarColumn(ColumnDescription = "是否置顶")]
        public bool IsStick { get; set; }

        [SugarColumn(ColumnDescription = "置顶结束时间")]
        public int StickEndTime { get; set; }

        [SugarColumn(ColumnDescription = "开始展示时间")]
        public int ShowStartTime { get; set; }

        [SugarColumn(ColumnDescription = "展示结束时间")]
        public int ShowEndTime { get; set; }

        [SugarColumn(Length = 400, IsNullable = false, ColumnDescription = "给哪些级别的人看")]
        public string ShowObject { get; set; }

        [SugarColumn(ColumnDescription = "阅读次数")]
        public int ReadingCount { get; set; }

        [SugarColumn(ColumnDescription = "评论次数")]
        public int CommentCount { get; set; }

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

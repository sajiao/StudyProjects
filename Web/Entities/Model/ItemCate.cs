using DotNet.Common.Attributes;
using SqlSugar;
using System;

namespace Entities.Model
{
    [SugarTable("ItemCate", "商品类型")]
    public class ItemCate
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "类型：1，淘宝")]
        public int TypeId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "自定义排序")]
        public int OrdId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "父类id，0表示父类")]
        public int ParentId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "商品分类id")]
        public int CateId { get; set; }
   
        [SugarColumn(Length = 255, IsNullable = false, ColumnDescription = "商品名称")]
        public string CateName { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "商品分类图片")]
        public string CateImg { get; set; }

        [SugarColumn(ColumnDescription = "最小月销量数据")]
        public Int64 MinVolume { get; set; }

        [SugarColumn(ColumnDescription = "状态，0失效，1有效")]
        public int Status { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public DateTime LastUpdateTime { get; set; }
    }
}

using DotNet.Common.Attributes;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    [SugarTable("JuTuan", "聚划算团")]
    public class JuTuan
    {
        [SugarColumn(Length = 36, ColumnDataType = "char", IsNullable = false, IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [MapName("GoodId", "商品id")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "商品id")]
        public string GoodId { get; set; }


        [MapName("GoodName", "商品标题")]
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "商品标题")]
        public string GoodName { get; set; }


        [MapName("OnePrice", "一人价（原价）")]
        [SugarColumn(Length = 12, DecimalDigits = 2, IsNullable = false, ColumnDescription = "一人价（原价）")]
        public decimal OnePrice { get; set; }


        [MapName("TuanPrice", "拼成价")]
        [SugarColumn(Length = 12, DecimalDigits = 2, IsNullable = false, ColumnDescription = "拼成价")]
        public decimal TuanPrice { get; set; }

        [MapName("PersonNum", "几人团")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "几人团")]
        public int PersonNum { get; set; }


        [MapName("GoodImg", "商品主图")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "商品主图")]
        public string GoodImg { get; set; }

        [MapName("BeginTime", "开始时间")]
        [SugarColumn(IsNullable = false, ColumnDescription = "开始时间")]
        public DateTime BeginTime { get; set; }

        [MapName("EndTime", "结束时间")]
        [SugarColumn(IsNullable = false, ColumnDescription = "结束时间")]
        public DateTime EndTime { get; set; }


        [MapName("TotalInventory", "库存数量")]
        [SugarColumn(IsNullable = false, ColumnDescription = "库存数量")]
        public int TotalInventory { get; set; }

        [MapName("SellCount", "已售数量")]
        [SugarColumn(IsNullable = false, ColumnDescription = "已售数量")]
        public int SellCount { get; set; }

        [MapName("Inventory", "剩余库存")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "剩余库存")]
        public string Inventory { get; set; }


        [MapName("ChangUrl", "推广长链接")]
        [SugarColumn(Length = 1000, IsNullable = false, ColumnDescription = "推广长链接")]
        public string ChangUrl { get; set; }

        [MapName("DuanUrl", "推广短链接")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "推广短链接")]
        public string DuanUrl { get; set; }

        [MapName("GotRate", "佣金比率（%）")]
        [SugarColumn(Length = 12, DecimalDigits = 2, IsNullable = false, ColumnDescription = "佣金比率（%）")]
        public decimal GotRate { get; set; }

        [MapName("Commission", "佣金金额")]
        [SugarColumn(Length = 12, DecimalDigits = 2, IsNullable = false, ColumnDescription = "佣金金额")]
        public decimal Commission { get; set; }


        [MapName("TypeId", "一级类目id")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "一级类目id")]
        public string TypeId { get; set; }

        [MapName("TypeName", "一级类目名称")]
        [SugarColumn(Length = 100, IsNullable = false, ColumnDescription = "一级类目名称")]
        public string TypeName { get; set; }

        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "Tags")]
        public string Tags { get; set; }


        [SugarColumn(ColumnDescription = "创建时间")]
        public DateTime CreatedTime { get; set; }

    }
}

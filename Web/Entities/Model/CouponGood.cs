using DotNet.Common.Attributes;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    [SugarTable("CouponGood", "价格")]
    public class CouponGood
    {
        [SugarColumn(Length = 36, ColumnDataType = "char",IsNullable = false, IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [MapName("GoodId", "商品id")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "商品id")]
        public string GoodId { get; set; }

        [MapName("GoodName", "商品名称")]
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "商品名称")]
        public string GoodName { get; set; }

        [MapName("GoodImg", "商品主图")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "商品主图")]
        public string GoodImg { get; set; }

        [MapName("GoodDetailUrl", "商品详情页链接地址")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "商品详情页链接地址")]
        public string GoodDetailUrl { get; set; }

        [MapName("TypeName", "商品一级类目")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "商品一级类目")]
        public string TypeName { get; set; }

        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "Tags")]
        public string Tags { get; set; }

        [MapName("TaoBaoKeUrl", "淘宝客链接")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "淘宝客链接")]
        public string TaoBaoKeUrl { get; set; }

        [MapName("GoodPrice", "商品价格(单位：元)")]
        [SugarColumn(Length = 13, DecimalDigits=2, IsNullable = false, ColumnDescription = "商品价格(单位：元)")]
        public decimal GoodPrice { get; set; }

        [MapName("SellCount", "商品月销量")]
        [SugarColumn(IsNullable = false, ColumnDescription = "商品月销量")]
        public int SellCount { get; set; }

        [MapName("GotRate", "收入比率(%)")]
        [SugarColumn(Length = 12, DecimalDigits = 2, IsNullable = false, ColumnDescription = "收入比率(%)")]
        public decimal GotRate { get; set; }

        [MapName("Commission", "佣金")]
        [SugarColumn(Length = 12, DecimalDigits = 2, IsNullable = false, ColumnDescription = "佣金")]
        public decimal Commission { get; set; }

        [MapName("SellerWangWang", "卖家旺旺")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "卖家旺旺")]
        public string SellerWangWang { get; set; }

        [MapName("SellerId", "卖家id")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "卖家id")]
        public string SellerId { get; set; }

        [MapName("ShopName", "店铺名称")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "店铺名称")]
        public string ShopName { get; set; }

        [MapName("Platform", "平台类型")]
        [SugarColumn(Length = 10, IsNullable = false, ColumnDescription = "平台类型")]
        public string Platform { get; set; }

        [MapName("CouponId", "优惠券id")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "优惠券id")]
        public string CouponId { get; set; }

        [MapName("CouponCount", "优惠券总量")]
        [SugarColumn(IsNullable = false, ColumnDescription = "优惠券总量")]
        public int CouponCount { get; set; }

        [MapName("CouponRestCount", "优惠券剩余量")]
        [SugarColumn(IsNullable = false, ColumnDescription = "优惠券剩余量")]
        public int CouponRestCount { get; set; }


        [MapName("CouponValue", "优惠券面额")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "优惠券面额")]
        public string CouponValue { get; set; }


        [MapName("BeginTime", "开始时间")]
        [SugarColumn(IsNullable = false, ColumnDescription = "开始时间")]
        public DateTime BeginTime { get; set; }

        [MapName("EndTime", "结束时间")]
        [SugarColumn(IsNullable = false, ColumnDescription = "结束时间")]
        public DateTime EndTime { get; set; }

        [MapName("CouponUrl", "优惠券链接")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "优惠券链接")]
        public string CouponUrl { get; set; }

        [MapName("ShopCouponUrl", "商品优惠券推广链接")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "商品优惠券推广链接")]
        public string ShopCouponUrl { get; set; }


        [SugarColumn(IsNullable = false, ColumnDescription = "创建时间")]
        public DateTime CreatedTime { get; set; }

    }
}

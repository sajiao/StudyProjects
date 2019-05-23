using DotNet.Common.Attributes;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    [SugarTable("tqk_Items", "商品项")]
    public class Items
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "自定义排序")]
        public int OrdId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "商品分类id", ColumnName = "cate_id")]
        public int CateId { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "AliId", ColumnName = "ali_id")]
        public string AliId { get; set; }


        [SugarColumn(IsNullable = false, ColumnDescription = "专场", ColumnName = "zc_id")]
        public int ZCId { get; set; }

 
        [SugarColumn(ColumnDescription = "OrigId", ColumnName = "orig_id")]
        public int OrigId { get; set; }

        [MapName("NumIid", "商品id")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "商品id", ColumnName = "num_iid")]
        public string NumIid { get; set; }

        [MapName("Title", "商品名称")]
        [SugarColumn(Length = 255, IsNullable = false, ColumnDescription = "商品名称")]
        public string Title { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "描述")]
        public string Intro { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "Tags")]
        public string Tags { get; set; }

        [SugarColumn(Length = 55, IsNullable = true, ColumnDescription = "卖家昵称")]
        public string Nick { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "change_price", ColumnName = "change_price")]
        public string ChangePrice { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "mobilezk", ColumnName = "mobilezk")]
        public string MobileZk { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "area", ColumnName = "area")]
        public string Area { get; set; }

        [MapName("SellerId", "卖家id")]
        [SugarColumn(Length = 25, IsNullable = true, ColumnDescription = "sellerId", ColumnName = "sellerId")]
        public string SellerId { get; set; }


        [SugarColumn(IsNullable = false, ColumnDescription = "uid默认1", ColumnName = "uid")]
        public int Uid { get; set; }

        [MapName("Uname", "卖家旺旺")]
        [SugarColumn(Length = 20 ,IsNullable = false, ColumnDescription = "uname", ColumnName = "uname")]
        public string Uname { get; set; }

        [MapName("PicUrl", "商品主图")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "商品主图", ColumnName = "pic_url")]
        public string PicUrl { get; set; }

        [MapName("PicUrls", "商品主图")]
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "宽版图片", ColumnName = "pic_urls")]
        public string PicUrls { get; set; }

        [MapName("Price", "商品价格(单位：元)")]
        [SugarColumn(Length = 13, DecimalDigits = 2, IsNullable = false, ColumnDescription = "商品价格(单位：元)")]
        public decimal Price { get; set; }


        [MapName("Link", "商品详情页链接地址")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "商品详情页链接地址")]
        public string Link { get; set; }

        [MapName("ClickUrl", "淘宝客链接")]
        [SugarColumn(Length = 500, IsNullable = false, ColumnDescription = "淘宝客链接",ColumnName = "click_url")]
        public string ClickUrl { get; set; }

     
        [SugarColumn(ColumnDescription = "Ding")]
        public int Ding { get; set; }

        [SugarColumn(ColumnDescription = "月销量数据")]
        public int Volume { get; set; }


        [MapName("CommissionRate", "收入比率(%)")]
        [SugarColumn(Length = 12, DecimalDigits = 2,  ColumnDescription = "收入比率(%)",ColumnName = "commission_rate")]
        public decimal CommissionRate { get; set; }

        [MapName("Commission", "佣金")]
        [SugarColumn(Length = 12, DecimalDigits = 2, ColumnDescription = "佣金")]
        public decimal Commission { get; set; }

        [SugarColumn(ColumnDescription = "CouponType", ColumnName = "coupon_type")]
        public int CouponType { get; set; }

        [SugarColumn(Length = 12, DecimalDigits = 2,  ColumnDescription = "CouponPrice", ColumnName = "coupon_price")]
        public decimal CouponPrice { get; set; }


        [SugarColumn(ColumnDescription = "是否审核", ColumnName = "pass")]
        public int Pass { get; set; }

        [SugarColumn( ColumnDescription = "状态")]
        public int Status { get; set; }

        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "未通过理由", ColumnName = "fail_reason")]
        public string FailReason { get; set; }

        [MapName("ShopName", "店铺名称")]
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "店铺名称",ColumnName = "shop_name")]
        public string ShopName { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "商品类型（淘宝，天猫）", ColumnName = "shop_type")]
        public string ShopType { get; set; }

        [MapName("ItemUrl", "商品优惠券推广链接")]
        [SugarColumn(Length = 500, ColumnDescription = "宝贝地址", ColumnName = "item_url")]
        public string ItemUrl { get; set; }

        [SugarColumn( ColumnDescription = "点击量")]
        public int Hits { get; set; }

        [SugarColumn(ColumnDescription = "默认1")]
        public int IsShow { get; set; }

        [SugarColumn(ColumnDescription = "默认0")]
        public int Likes { get; set; }


        [MapName("Inventory", "商品月销量")]
        [SugarColumn(IsNullable = false, ColumnDescription = "库存")]
        public int Inventory { get; set; }

        [SugarColumn(IsNullable = true, ColumnDescription = "AddTime", ColumnName = "add_time")]
        public int AddTime { get; set; }


        [SugarColumn(IsNullable = false, ColumnDescription = "LastRateTime", ColumnName = "last_rate_time")]
        public int LastRateTime { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "是否采集了淘宝评论1表示已经采集了淘宝评论", ColumnName = "is_collect_comments")]
        public int IsCollectComments { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "Quan")]
        public string Quan { get; set; }


        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "QuanUrl")]
        public string QuanUrl { get; set; }

        [SugarColumn(Length = 250, IsNullable = true, ColumnDescription = "QuanKouLing")]
        public int QuanKouLing { get; set; }


        [SugarColumn(Length = 250, IsNullable = true, ColumnDescription = "QuanShortUrl")]
        public string QuanShortUrl { get; set; }

        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "QuanCondition", ColumnName = "Quan_condition")]
        public string QuanCondition { get; set; }



        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "优惠券已领数量", ColumnName = "Quan_receive")]
        public int QuanReceive { get; set; }


        [SugarColumn(ColumnDescription = "LastTime", ColumnName = "last_time")]
        public int LastTime { get; set; }


        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "QuanId", ColumnName = "Quan_id")]
        public string QuanId { get; set; }


        [SugarColumn(ColumnDataType ="text", IsNullable = true, ColumnDescription = "Desc")]
        public string Desc { get; set; }

        [SugarColumn(ColumnDescription = "是否推送")]
        public int TuiSong { get; set; }

        [SugarColumn( ColumnDescription = "IsCommend", ColumnName = "is_commend")]
        public int IsCommend { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "默认0",ColumnName = "up_time")]
        public string UpTime { get; set; }

        [MapName("Platform", "平台类型")]
        [SugarColumn(Length = 10, IsNullable = true, ColumnDescription = "平台类型", IsIgnore =true)]
        public string Platform { get; set; }


        [MapName("TypeName", "商品一级类目")]
        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "商品一级类目", IsIgnore = true)]
        public string TypeName { get; set; }

        [MapName("BeginTime", "开始时间")]
        [SugarColumn(IsNullable = false, ColumnDescription = "开始时间", IsIgnore = true)]
        public DateTime BeginTime { get; set; }

        [MapName("EndTime", "结束时间")]
        [SugarColumn(IsNullable = false, ColumnDescription = "结束时间", IsIgnore = true)]
        public DateTime EndTime { get; set; }

    }
}

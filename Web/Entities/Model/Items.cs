using DotNet.Common.Attributes;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    [SugarTable("Items", "商品项")]
    public class Items
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "类型：1，淘宝优惠券，2，淘宝产品")]
        public int TypeId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "自定义排序")]
        public int OrdId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "商品分类id")]
        public int CateId { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "AliId")]
        public string AliId { get; set; }


        [SugarColumn(IsNullable = false, ColumnDescription = "专场")]
        public int ZCId { get; set; }

        [MapName("NumIid", "商品id")]
        [SugarColumn(ColumnDescription = "商品id")]
        public Int64 NumIid { get; set; }

        [MapName("Title", "商品名称")]
        [SugarColumn(Length = 255, IsNullable = false, ColumnDescription = "商品名称")]
        public string Title { get; set; }

        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "描述")]
        public string Intro { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "Tags")]
        public string Tags { get; set; }

        [MapName("PicUrl", "商品主图")]
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "商品主图")]
        public string PicUrl { get; set; }

        [MapName("PicUrls", "商品主图")]
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "商品小图列表")]
        public string SmallImages { get; set; }

        [MapName("Price", "商品价格(单位：元)")]
        [SugarColumn(Length = 11, DecimalDigits = 2, IsNullable = false, ColumnDescription = "商品价格(单位：元)")]
        public decimal Price { get; set; }

        [SugarColumn(Length = 11, DecimalDigits = 2, IsNullable = false, ColumnDescription = "商品最终价格")]
        public decimal FinalPrice { get; set; }

        [SugarColumn(Length = 11, DecimalDigits = 2, IsNullable = false, ColumnDescription = "优惠多少")]
        public decimal YouhuiPrice { get; set; }

        [SugarColumn( ColumnDescription = "卖家类型，0表示集市，1表示商城")]
        public Int64 UserType { get; set; }


        [SugarColumn(Length = 55, IsNullable = true, ColumnDescription = "卖家昵称/店铺名称")]
        public string Nick { get; set; }

        [SugarColumn(Length = 55, IsNullable = true, ColumnDescription = "叶子类目名称,eg:情趣内衣")]
        public string CatLeafName { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "宝贝所在地")]
        public string Area { get; set; }


        [MapName("SellerId", "卖家id")]
        [SugarColumn(Length = 25, IsNullable = true, ColumnDescription = "卖家id")]
        public string SellerId { get; set; }

        [MapName("Volume", "商品月销量")]
        [SugarColumn(ColumnDescription = "月销量数据")]
        public Int64 Volume { get; set; }

        [SugarColumn(ColumnDescription = "是否加入消费者保障")]
        public bool IsPrepay { get; set; }

        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "	店铺dsr 评分")]
        public string ShopDsr { get; set; }

        [SugarColumn(ColumnDescription = "卖家等级")]
        public int RateSum { get; set; }

        [SugarColumn(ColumnDescription = "退款率是否低于行业均值")]
        public bool IRfdRate { get; set; }

        [SugarColumn(ColumnDescription = "好评率是否高于行业均值")]
        public bool HGoodRate { get; set; }

        [SugarColumn(ColumnDescription = "成交转化是否高于行业均值")]
        public bool HPayRate30 { get; set; }

        [SugarColumn(ColumnDescription = "是否包邮")]
        public bool FreeShipment { get; set; }

        [SugarColumn(Length = 20 ,IsNullable = true, ColumnDescription = "商品库类型，支持多库类型输出，以“，”区分，1:营销商品主推库")]
        public string MaterialLibType { get; set; }

        [MapName("ProductUrl", "商品详情页链接地址")]
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "商品详情页链接地址")]
        public string ProductUrl { get; set; }

        [MapName("ClickUrl", "淘宝客链接")]
        [MapName("ClickUrl", "优惠券链接")]
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "淘宝客链接/商品链接（是淘客商品返回淘客链接，非淘客商品返回普通h5链接）")]
        public string ClickUrl { get; set; }

        [MapName("CommissionRate", "收入比率(%)")]
        [SugarColumn(Length = 12, DecimalDigits = 2, ColumnDescription = "收入比例，举例，取值为20.00，表示比例20.00%")]
        public decimal CommissionRate { get; set; }

        [MapName("Commission", "佣金")]
        [SugarColumn(Length = 12, DecimalDigits = 2, ColumnDescription = "佣金")]
        public decimal Commission { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "	无线折扣价，即宝贝在无线上的实际售卖价格。")]
        public string FinalPriceWap { get; set; }

        [MapName("ShopName", "店铺名称")]
        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "店铺名称")]
        public string ShopName { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "商品类型（淘宝，天猫）")]
        public string ShopType { get; set; }

        [SugarColumn(ColumnDescription = "宝贝类型：1 普通商品； 2 鹊桥高佣金商品；3 定向招商商品；4 营销计划商品")]
        public int ItemType { get; set; }

        [MapName("ItemUrl", "商品优惠券推广链接")]
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "商品优惠券推广链接")]
        public string ItemUrl { get; set; }

        [SugarColumn(ColumnDescription = "宝贝状态，0失效，1有效；注：失效可能是宝贝已经下线或者是被处罚不能在进行推广")]
        public int Status { get; set; }

        [MapName("ItemInfo", "优惠券面额")]
        [SugarColumn(Length = 200,IsNullable =true,  ColumnDescription = "优惠券面额")]
        public string ItemInfo { get; set; }

        [SugarColumn(ColumnDescription = "后台一级类目")]
        public int Category { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "类目名称")]
        public string CategoryName { get; set; }

        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "未通过理由")]
        public string FailReason { get; set; }


        [SugarColumn( ColumnDescription = "点击量")]
        public int Hits { get; set; }

        [SugarColumn(ColumnDescription = "默认1")]
        public int IsShow { get; set; }

        [SugarColumn(ColumnDescription = "默认0")]
        public int Likes { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "总库存")]
        public int Inventory { get; set; }

        [MapName("TotalCount", "优惠券总量")]
        [SugarColumn( ColumnDescription = "	优惠券总量")]
        public int TotalCount { get; set; }

        [MapName("RemainCount", "优惠券剩余量")]
        [SugarColumn(IsNullable = false, ColumnDescription = "优惠券剩余量")]
        public int RemainCount { get; set; }


        [SugarColumn(IsNullable = false, ColumnDescription = "是否采集了淘宝评论1表示已经采集了淘宝评论")]
        public int IsCollectComments { get; set; }

        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "使用条件")]
        public string Condition { get; set; }

        [SugarColumn(ColumnDescription = "LastTime")]
        public DateTime LastTime { get; set; }

        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "Item描述")]
        public string ItemDesc { get; set; }

        [SugarColumn(ColumnDataType ="text", IsNullable = true, ColumnDescription = "描述")]
        public string PcDesc { get; set; }

        [SugarColumn(ColumnDataType = "text", IsNullable = true, ColumnDescription = "描述")]
        public string WapDesc { get; set; }

        [MapName("Platform", "平台类型")]
        [SugarColumn(Length = 10, IsNullable = true, ColumnDescription = "平台类型")]
        public string Platform { get; set; }

        [MapName("TypeName", "商品一级类目")]
        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "商品一级类目", IsIgnore = true)]
        public string TypeName { get; set; }

        [MapName("BeginTime", "优惠券开始时间")]
        [SugarColumn(IsNullable = true, ColumnDescription = "开始时间")]
        public DateTime BeginTime { get; set; }

        [MapName("EndTime", "优惠券结束时间")]
        [SugarColumn(IsNullable = true, ColumnDescription = "结束时间")]
        public DateTime EndTime { get; set; }

    }
}

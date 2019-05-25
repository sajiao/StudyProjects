using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    [SugarTable("Shop", "店铺")]
    public  class Shop
    {
        [SugarColumn(ColumnDescription = "卖家ID")]
        public int UserId { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "店铺名称")]
        public string ShopTitle { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "店铺类型，B：天猫，C：淘宝")]
        public string ShopType { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "卖家昵称")]
        public string SellerNick { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "店标图片")]
        public string PicUrl { get; set; }

        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "	店铺地址")]
        public string ShopUrl { get; set; }

    }
}

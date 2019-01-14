using SqlSugar;
using System;

namespace Entities.Model
{
    [SugarTable("Charge", "用户充值表")]
   public class Charge
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [SugarColumn(Length = 64, IsNullable = false, ColumnDescription = "订单ID，唯一标识")]
        public string OrderID { get; set; }

        [SugarColumn(IsNullable = false,  ColumnDescription = "产品Id")]
        public int ProductID { get; set; }

        [SugarColumn(Length = 13, IsNullable = false, ColumnDescription = "充值金额")]
        public decimal ChargeMoney { get; set; }

        [SugarColumn(Length = 13, IsNullable = false, ColumnDescription = "活动充值金额")]
        public decimal ActivityChargeMoney { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "充值获得赠送的货币数量")]
        public int GivePoint { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "充值类型")]
        public int ChargeType { get; set; }

        [SugarColumn(Length = 64, IsNullable = false, ColumnDescription = "设备Id")]
        public string DeviceIdentifier { get; set; }

        [SugarColumn(Length = 13, IsNullable = false, ColumnDescription = "最终充值金额")]
        public decimal FinalChargeMoney { get; set; }

        [SugarColumn(ColumnDescription = "创建时间")]
        public int CreatedTime { get; set; }

    }
}

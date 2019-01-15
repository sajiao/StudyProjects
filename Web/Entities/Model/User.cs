using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
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

        [SugarColumn( ColumnDescription = "性别（男0，女1）")]
        public int Gender { get; set; }

        [SugarColumn(Length = 100, IsNullable = true, ColumnDescription = "头像")]
        public string HeadImage { get; set; }

        [SugarColumn(Length = 512, ColumnDataType = "Nvarchar", IsNullable = true, ColumnDescription = "玩家个性签名")]
        public string Sign { get; set; }

        [SugarColumn(ColumnDescription = "最后活跃时间")]
        public int ActiveTime { get; set; }

        [SugarColumn(ColumnDescription = "来源ID")]
        public int FromSource { get; set; }

        [SugarColumn(ColumnDescription = "用户邮箱")]
        public string Email { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "是否验证邮箱")]
        public bool IsConfirmEmail { get; set; }

        [SugarColumn(ColumnDescription = "用户电话号码")]
        public string PhoneNumber { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "是否验证手机号码")]
        public bool IsConfirmPhoneNumber { get; set; }

        [SugarColumn(ColumnDescription = "是否内部账号")]
        public int IsInner { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "邀请人ID")]
        public Guid InvitePlayerID { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "text", ColumnDescription = "额外信息")]
        public string ExtrInfo { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "是否机器人")]
        public bool IsRobot { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "用户等级")]
        public int Lv { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "Vip经验")]
        public int VipEXP { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "Vip等级")]
        public int Vip { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "Vip结束时间")]
        public int VipEndTime { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "钻石")]
        public int Diamond { get; set; }

        [SugarColumn(ColumnDescription = "创建时间")]
        public int CreatedTime { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public int LastUpdatedTime { get; set; }

        [SugarColumn(ColumnDescription = "上一次更新时间")]
        public int LastLoginTime { get; set; }
    }
}

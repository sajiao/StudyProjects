using SqlSugar;

namespace Entities.Model
{
    [SugarTable("ModuleSub", "子模块")]
    public class ModuleSub
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 30, IsNullable = false, ColumnDescription = "子模块别名（英文字符，用于在代码中定义为常量)")]
        public string Alias { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "名称")]
        public string Name { get; set; }

        [SugarColumn(ColumnDescription = "所属的模块Id")]
        public int ModuleId { get; set; }

        [SugarColumn(Length = 30, IsNullable = false, ColumnDescription = "所属的模块别名")]
        public string ModuleAlias { get; set; }

        [SugarColumn(ColumnDescription = "是否开放")]
        public bool IfOpen { get; set; }

        [SugarColumn(ColumnDescription = "是否是客户端模块")]
        public bool IsClient { get; set; }

        [SugarColumn(ColumnDescription = "开启所需玩家等级(且的关系")]
        public int OpenNeedPlayLv { get; set; }

        [SugarColumn(ColumnDescription = "所需玩家是否当前是vip(且的关系")]
        public bool OpenNeedPlayIsVip { get; set; }

        [SugarColumn(ColumnDescription = "开启所需VIP等级")]
        public int AdvancedOpenVIPLv { get; set; }

        [SugarColumn(ColumnDescription = "需完成任务ID(且")]
        public int NeedTaskID { get; set; }

    }
}

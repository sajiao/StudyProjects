using SqlSugar;

namespace Entities.Model
{
    [SugarTable("SystemConfig", "系统配置表")]
    public class SystemItem
    {
        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "配置的键")]
        public string ConfigKey { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "text", ColumnDescription = "配置的值")]
        public string ConfigValue { get; set; }

        [SugarColumn(Length = 1000, ColumnDescription = "配置描述信息")]
        public string ConfigDesc { get; set; }
    }
}

using SqlSugar;

namespace Entities.Model
{
    [SugarTable("EmailEnum", "邮件模块枚举")]
    public class EmailEnum
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 30, IsNullable = false, ColumnDescription = "枚举名称")]
        public string Alias { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "名称")]
        public string Name { get; set; }

        [SugarColumn(ColumnDescription = "最大邮件数量")]
        public int MaxEmailNum { get; set; }

        [SugarColumn(ColumnDescription = "单页显示数量")]
        public int ShowNum { get; set; }

        [SugarColumn(ColumnDescription = "是否显示列表")]
        public bool IsShowList { get; set; }

    }
}

using SqlSugar;

namespace Entities.Model
{
    [SugarTable("EmailModel", "邮件模型表")]
    public class EmailModel
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnDescription = "枚举ID")]
        public int EnumID { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, ColumnDescription = "名称")]
        public string Name { get; set; }

        [SugarColumn(Length = 1000, IsNullable = false, ColumnDescription = "邮件内容")]
        public string Desc { get; set; }

        [SugarColumn(ColumnDescription = "邮件保存天数")]
        public int StorageDays { get; set; }

    }
}

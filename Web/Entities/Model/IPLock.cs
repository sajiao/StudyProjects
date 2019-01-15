using SqlSugar;

namespace Entities.Model
{
    [SugarTable("IPLock", "封ip列表")]
   public class IPLock
    {
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "要封锁的IP")]
        public string Ip { get; set; }
    }
}

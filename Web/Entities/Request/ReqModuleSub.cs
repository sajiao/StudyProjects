using Entities.Model;

namespace Entities.Request
{
   public class ReqModuleSub: ReqBase
    {

        public int Id { get; set; }

        public int ModuleId { get; set; }
        public ModuleSub ConvertData()
        {
            ModuleSub data = new ModuleSub()
            {
                Id = this.Id,
                ModuleId = this.ModuleId
            };

            return data;
        }
    }
}

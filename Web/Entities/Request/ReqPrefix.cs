using Entities.Model;

namespace Entities.Request
{
   public  class ReqPrefix :ReqBase
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public Prefix ConvertData()
        {
            Prefix data = new Prefix()
            {
                Id = this.Id
            };

            return data;
        }
    }
}

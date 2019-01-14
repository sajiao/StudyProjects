using Entities.Model;

namespace Entities.Request
{
  public  class ReqSuffix: ReqBase
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public Suffix ConvertData()
        {
            Suffix data = new Suffix()
            {
                Id = this.Id
            };

            return data;
        }
    }
}

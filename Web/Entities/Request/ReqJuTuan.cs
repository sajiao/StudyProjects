using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Request
{
    public class ReqJuTuan : ReqBase
    {
        public Guid Id { get; set; }

        public string GoodName { get; set; }

        public DateTime Endime { get; set; }

        public JuTuan ConvertData()
        {
            JuTuan data = new JuTuan()
            {
                Id = this.Id,
                GoodName = this.GoodName,
                EndTime = this.Endime
            };

            return data;
        }
    }
}

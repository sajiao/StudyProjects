using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Request
{
    public class ReqItems : ReqBase
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string Title { get; set; }

        public string Tags { get; set; }

        public Items ConvertData()
        {
            Items data = new Items()
            {
                Id = this.Id,
                Title = this.Title,
                TypeId = this.TypeId,
                Tags = this.Tags,
            };

            return data;
        }
    }
}

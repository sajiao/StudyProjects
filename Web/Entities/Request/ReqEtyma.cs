using Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Request
{
   public class ReqEtyma : ReqBase
    {
        public int Id { get; set; }

        public string Word { get; set; }

        // [Required(AllowEmptyStrings = true, ErrorMessage = nameof(Name) + "为必填参数")]
        //public string Name { get; set; }
        public Etyma ConvertData()
        {
            Etyma data = new Etyma()
            {
                Id = this.Id
            };

            return data;
        }
    }
}

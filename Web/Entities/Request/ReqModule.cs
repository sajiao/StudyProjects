using Entities;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Request
{
   public class ReqModule
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = nameof(Name) + "为必填参数")]
        public string Name { get; set; }

        public Module Conver2DBModel()
        {
            var result = new Module()
            {
                Name = this.Name,
            };

            return result;
        }
    }
}

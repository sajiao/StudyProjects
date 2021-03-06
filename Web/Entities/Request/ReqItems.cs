﻿using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Request
{
    public class ReqItems : ReqBase
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string KeyWord { get; set; }
        public string Title { get; set; }

        public string Tag { get; set; }

        public int ZCId { get; set; }

        public bool IsFull { get; set; }

        public Items ConvertData()
        {
            Items data = new Items()
            {
                Id = this.Id,
                Title = this.Title,
                TypeId = this.TypeId,
                Tags = this.Tag,
                ZCId = this.ZCId,
            };

            return data;
        }
    }
}

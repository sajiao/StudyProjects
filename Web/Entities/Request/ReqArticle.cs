﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Request
{
    public class ReqArticle: ReqBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Article ConvertData()
        {
            Article data = new Article()
            {
                Id = this.Id,
                Title = this.Title
            };

            return data;
        }
    }
}

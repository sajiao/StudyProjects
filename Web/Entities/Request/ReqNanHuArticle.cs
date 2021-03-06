﻿using Entities.Model;

namespace Entities.Request
{
    public class ReqNanHuArticle: ReqBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }
        public NanHuArticle ConvertData()
        {
            NanHuArticle data = new NanHuArticle()
            {
                Id = this.Id,
                Title = this.Title,
                CategoryId = this.CategoryId
            };

            return data;
        }
    }
}

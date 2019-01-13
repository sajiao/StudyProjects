using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class PageInfo
    {
        private int? pageSize { get; set; }

        public int PageIndex { get; set; }
        public int PageSize
        {
            get
            {
                if (!this.pageSize.HasValue)
                {
                    this.pageSize = 20;
                }
                return this.pageSize.GetValueOrDefault();
            }
            set
            {
                this.pageSize = value;
            }
        }

        public string Sort { get; set; }
        public string SortFields { get; set; }

        public bool IsGetAll { get; set; }
    }
}

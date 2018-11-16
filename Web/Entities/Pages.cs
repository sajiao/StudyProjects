using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class Pages
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        
        public string Order { get; set; }

        public string OrderBy { get; set; }
    }
}

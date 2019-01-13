using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Result<T> where T : class, new()
    {
        public Int64 TotalCount { get; set; }
        public List<T> Results { get; set; }
    }
}

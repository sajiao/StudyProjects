using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tool.Models;

namespace Tool
{
    public class MappingDetailComparer : IEqualityComparer<MappingDetail>
    {
        public bool Equals(MappingDetail x, MappingDetail y)
        {
            return x != null && y != null && x.Id == y.Id;
        }

        public int GetHashCode(MappingDetail obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

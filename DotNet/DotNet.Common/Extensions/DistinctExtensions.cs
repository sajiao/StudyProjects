using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
{
   public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> souces, Func<T, V> selector)
        {
            return souces.Distinct(new CommonEqualityComparer<T, V>(selector));
        }
    }
}

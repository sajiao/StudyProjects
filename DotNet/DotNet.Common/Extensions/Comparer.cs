using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Common
{
    public class GenericCompare<T> : IEqualityComparer<T> where T : class
    {
        private Func<T, object> _expr { get; set; }
     
        public GenericCompare(Func<T, object> expr)
        {
            this._expr = expr;
        }

        public bool Equals(T x, T y)
        {
            var first = _expr.Invoke(x);
            var sec = _expr.Invoke(y);
            if (first == null && sec == null)
                return true;
            else if (first == null | sec == null)
                return false;
            else if (first != null && first.Equals(sec))
                return true;
            else
                return false;
        }

        public int GetHashCode(T t)
        {
            return _expr.Invoke(t).GetHashCode();
        }
    }

    public class CommonEqualityComparer<T, V> : EqualityComparer<T>
    {
        private Func<T, V> keySelector;

        public CommonEqualityComparer(Func<T, V> selector)
        {
            this.keySelector = selector;
        }

        public override bool Equals(T x, T y)
        {
            return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
        }

        public override int GetHashCode(T obj)
        {
            return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
        }
    }
}

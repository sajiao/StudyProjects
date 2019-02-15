using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DotNet.Common
{
   public static class ExpressionExtensions
    {
        public static Expression<TDelegate> AndAlso<TDelegate>(this Expression<TDelegate> left, Expression<TDelegate> right)
        {
            return Expression.Lambda<TDelegate>(Expression.AndAlso(left, right), left.Parameters);
        }
    }
}

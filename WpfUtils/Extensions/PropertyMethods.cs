using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.Extensions
{
    public static class PropertyMethods
    {
        public static string GetName<T>(Expression<Func<T>> selector)
        {
            if (selector == null)
                return String.Empty;

            MemberExpression b = selector.Body as MemberExpression;
            if (b == null)
                throw new ArgumentException("The body must be a member expression");
            return b.Member.Name;
        }
    }
}

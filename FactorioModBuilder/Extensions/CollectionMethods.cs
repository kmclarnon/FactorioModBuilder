using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Extensions
{
    public static class CollectionMethods
    {
        public static List<T> ToList<T>(this T item)
        {
            return new List<T>() { item };
        }

        public static IEnumerable<T> ConcatMany<T>(this IEnumerable<T> target, params IEnumerable<T>[] elms)
        {
            foreach (var e in elms)
                target = target.Concat(e);
            return target;
        }
    }
}

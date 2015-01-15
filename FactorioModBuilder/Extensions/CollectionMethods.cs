using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Extensions
{
    public static class CollectionMethods
    {
        public static List<T> ListWrap<T>(this T item)
        {
            return new List<T>() { item };
        }

        public static IEnumerable<T> ConcatMany<T>(this IEnumerable<T> target, params IEnumerable<T>[] elms)
        {
            foreach (var e in elms)
                target = target.Concat(e);
            return target;
        }

        public static IEnumerable<T> DistinctBy<T, U>(this IEnumerable<T> list, Func<T, U> selector)
        {
            var keys = new HashSet<U>();
            foreach(var elem in list)
            {
                if (keys.Add(selector(elem)))
                    yield return elem;
            }
        }
    }
}

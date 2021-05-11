using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blog
{
    public static class Extensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }
}

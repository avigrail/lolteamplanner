using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamTierList.Core
{
    public static class Extensions
    {
        public static T TrySingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : class
        {
            try
            {
                return source.SingleOrDefault(predicate);
            }
            catch
            {
                return null;
            }
        }
    }
}

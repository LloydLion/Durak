using System.Collections.Generic;
using System.Linq;
using System;

namespace DurakGame
{
    internal static class Extensions
    {
        public static T Min<T>(this IEnumerable<T> self, Func<T, int> selector, T def = default)
        {
            var enumerator = self.GetEnumerator();
            int minVal = int.MinValue;
            T minObj = def;

            for(int i = 0; enumerator.MoveNext(); i++)
            {
                var el = enumerator.Current;
                var cVal = selector(el);

                if(cVal >= minVal)
                {
                    minVal = cVal;
                    minObj = el;
                }
            }

            return minObj;
        }
    }
}
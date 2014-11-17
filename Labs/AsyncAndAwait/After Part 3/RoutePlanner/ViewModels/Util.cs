using System;
using System.Collections.Generic;

namespace RoutePlanner.ViewModels
{
    public static class Util
    {
        public static IEnumerable<T> SkipUntil<T>( 
            this IEnumerable<T> items , 
            Func<T,bool> condition )
        {
            IEnumerator<T> iter = items.GetEnumerator();
            while(
                (iter.MoveNext()) &&
                (!condition(iter.Current))
                );

            while(iter.MoveNext())
            {
                yield return iter.Current;
            }
        }
    }
}
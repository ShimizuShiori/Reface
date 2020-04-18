using System;
using System.Collections.Generic;

namespace Reface
{

    public static class CollectionExtensions
    {
        /// <summary>
        /// 循环并处理每个元素，并返回集合本身
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> handler)
        {
            foreach (var item in list)
                handler(item);
            return list;
        }

    }
}

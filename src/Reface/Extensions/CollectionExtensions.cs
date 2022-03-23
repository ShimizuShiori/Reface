using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface
{

    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> handler)
        {
            foreach (var item in list)
                handler(item);
        }
        public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> handler)
        {
            int i = 0;
            foreach (var item in list)
                handler(item, i++);
        }

        public static IReadOnlyCollection<T> ToReadonlyList<T>(this IEnumerable<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }

        public static string ToHexString(this IEnumerable<byte> buffer)
        {
            return string.Join(
                ""
                , buffer
                    .Select(b => b.ToString("X2"))
            );
        }

        public static bool Exists<T>(this IEnumerable<T> list, Func<T, bool> filter)
        {
            foreach (var item in list)
            {
                if (filter(item)) return true;
            }
            return false;
        }
        public static bool Empty<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }


        public static void AddAll<T>(this ICollection<T> list, IEnumerable<T> argument)
        {
            argument.ForEach(x => list.Add(x));
        }


        public static IDictionary<TKey, TValue> ToDict<TItem, TKey, TValue>(this IEnumerable<TItem> list, Func<TItem, TKey> keyGetter, Func<TItem, TValue> valueGetter, DuplicateKeyStrategies duplicateKeyStrategy = DuplicateKeyStrategies.Error)
        {
            IDictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            list.ForEach(item =>
            {
                TKey key = keyGetter(item);
                TValue value = valueGetter(item);
                if (!dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                    return;
                }
                switch (duplicateKeyStrategy)
                {
                    case DuplicateKeyStrategies.Leap:
                        break;
                    case DuplicateKeyStrategies.Recover:
                        dictionary[key] = value;
                        break;
                    case DuplicateKeyStrategies.Error:
                        throw new InvalidOperationException("Key 已存在，不能重复写入");
                    default:
                        throw new NotImplementedException("未实现的策略 : " + duplicateKeyStrategy.ToString());
                }
            });
            return dictionary;
        }

        public static IDictionary<TKey, TItem> ToDict<TItem, TKey>(this IEnumerable<TItem> list, Func<TItem, TKey> keyGetter, DuplicateKeyStrategies duplicateKeyStrategy = DuplicateKeyStrategies.Error)
        {
            return list.ToDict<TItem, TKey, TItem>(keyGetter, item => item, duplicateKeyStrategy);
        }

        public static IEnumerable<TItem> Distinct<TItem, TKey>(this IEnumerable<TItem> list, Func<TItem, TKey> keyGetter)
        {
            return list.ToDict(keyGetter, duplicateKeyStrategy: DuplicateKeyStrategies.Leap).Select(x => x.Value);
        }


        public static int IndexOf<T>(this IEnumerable<T> list, Func<T, bool> filter)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (filter(item))
                    return i;
                i++;
            }
            return -1;
        }
    }
}

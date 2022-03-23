using System;
using System.Collections;
using System.Collections.Generic;

namespace Reface
{
    public class ObserverDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> raw;

        public event EventHandler<DictionaryItemEventArgs<TKey, TValue>> ItemAdded;
        public event EventHandler<DictionaryItemEventArgs<TKey, TValue>> ItemUpdated;
        public event EventHandler<RemoveDictionaryItemEventArgs<TKey>> ItemRemoved;
        public event EventHandler ItemsCleared;

        public ObserverDictionary(IDictionary<TKey, TValue> raw)
        {
            this.raw = raw;
        }

        public TValue this[TKey key]
        {
            get { return raw[key]; }
            set
            {
                bool isAppend = !raw.ContainsKey(key);
                raw[key] = value;
                DictionaryItemEventArgs<TKey, TValue> args = new DictionaryItemEventArgs<TKey, TValue>(key, value);
                if (isAppend)
                    ItemAdded?.Invoke(this, args);
                else
                    ItemUpdated?.Invoke(this, args);
            }
        }

        public ICollection<TKey> Keys => raw.Keys;

        public ICollection<TValue> Values => raw.Values;

        public int Count => raw.Count;

        public bool IsReadOnly => raw.IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            raw.Add(key, value);
            ItemAdded?.Invoke(this, new DictionaryItemEventArgs<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            raw.Add(item);
            ItemAdded?.Invoke(this, new DictionaryItemEventArgs<TKey, TValue>(item.Key, item.Value));
        }

        public void Clear()
        {
            raw.Clear();
            ItemsCleared?.Invoke(this, EventArgs.Empty);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return raw.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return raw.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            raw.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return raw.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            bool result = raw.Remove(key);
            if (result)
                ItemRemoved?.Invoke(this, new RemoveDictionaryItemEventArgs<TKey>(key));
            return result;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool result = raw.Remove(item);
            if (result)
                ItemRemoved?.Invoke(this, new RemoveDictionaryItemEventArgs<TKey>(item.Key));
            return result;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return raw.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return raw.GetEnumerator();
        }
    }
}

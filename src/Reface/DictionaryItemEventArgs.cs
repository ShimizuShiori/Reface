using System;

namespace Reface
{
    public class DictionaryItemEventArgs<TKey, TValue> : EventArgs
    {
        public TKey Key { get; private set; }
        public TValue Value { get; private set; }

        public DictionaryItemEventArgs(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}

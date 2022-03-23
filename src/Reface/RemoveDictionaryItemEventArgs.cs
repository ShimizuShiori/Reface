using System;

namespace Reface
{
    public class RemoveDictionaryItemEventArgs<TKey> : EventArgs
    {
        public TKey Key { get; private set; }

        public RemoveDictionaryItemEventArgs(TKey key)
        {
            Key = key;
        }
    }
}

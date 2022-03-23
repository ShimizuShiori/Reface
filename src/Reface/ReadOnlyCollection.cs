using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Reface
{
    public class ReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly IEnumerable<T> list;

        public ReadOnlyCollection(IEnumerable<T> list) : this(list, list.Count())
        {

        }

        public ReadOnlyCollection(IEnumerable<T> list, int count)
        {
            this.list = list;
            this.Count = count;
        }

        public int Count { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

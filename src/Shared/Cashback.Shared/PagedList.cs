using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Shared
{
    public class PagedList<T> : IPagedList<T>
    {
        public int Limit { get; private set; }
        public int Offset { get; private set; }

        public IEnumerable<T> Items { get; private set; }

        public PagedList(int limit, int offset, IEnumerable<T> items)
        {
            Limit = limit;
            Offset = offset;
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }
    }
}

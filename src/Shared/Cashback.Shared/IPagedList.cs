using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Shared
{
    public interface IPagedList<T> 
    {
        int Limit { get;  }
        int Offset { get; }
        IEnumerable<T> Items { get; }
    }
}

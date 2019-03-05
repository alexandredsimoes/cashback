using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Interfaces
{
    public interface IPagination
    {
        int Offset { get; set; }
        int Quantity { get; set; }
    }
}

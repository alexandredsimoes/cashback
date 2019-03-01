using Cashback.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Models
{
    public class OrderFilter : IPagination
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Offset { get; set; }
        public int Quantity { get; set; }
    }
}

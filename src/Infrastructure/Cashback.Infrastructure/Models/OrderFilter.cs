using Cashback.Infrastructure.Data.Interfaces;
using System;

namespace Cashback.Infrastructure.Data.Models
{
    public class OrderFilter : IPagination
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Offset { get; set; }
        public int Quantity { get; set; }
    }
}

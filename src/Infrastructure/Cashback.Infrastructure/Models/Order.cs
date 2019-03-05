using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderItem> Items { get; set; }

    }
}

using Cashback.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Shared
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public double TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalCashback { get; set; }
        public ICollection<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}

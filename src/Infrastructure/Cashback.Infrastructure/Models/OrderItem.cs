﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public double UnityPrice { get; set; }
        public int Quantity { get; set; }
        public double CashbackPercent { get; set; }
        public double CashBackTotal { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}

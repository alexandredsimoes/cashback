using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Shared.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public string AlbumName { get; set; }
        public double UnityPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}

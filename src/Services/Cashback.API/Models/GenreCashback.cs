using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Models
{
    public class GenreCashback
    {
        public int Id { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public double Percent { get; set; }
        public DayOfWeek DayOfWeek { get; set; }        
    }
}

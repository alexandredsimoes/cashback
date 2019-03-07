using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public string Identifier { get; set; }
        public double Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.ViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}

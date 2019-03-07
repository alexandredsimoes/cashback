using Cashback.API.Interfaces;
using Cashback.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Services
{
    public class CatalogService : ICatalogService
    {
        public Task<AlbumViewModel> GetAlbumById(int albumId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlbumViewModel>> ListAlbuns(int genreId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}

using Cashback.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<AlbumViewModel>> ListAlbuns(int genreId, int pageIndex, int pageSize);
        Task<AlbumViewModel> GetAlbumById(int albumId);

    }
}

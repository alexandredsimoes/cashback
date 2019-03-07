using Cashback.Infrastructure.Data.Models;
using Cashback.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Interfaces
{
    public interface ICatalogRepository
    {
        bool IsInitializeCatalog();
        Task<bool> Save(Album album);
        Task<IPagedList<Album>> ListAlbums(string genreName, int offset, int limit);
        Task<Album> GetByIdAsync(int albumId);
        Task<double> GetCashbackPercent(int genreId, DayOfWeek dayOfWeek);


    }
}

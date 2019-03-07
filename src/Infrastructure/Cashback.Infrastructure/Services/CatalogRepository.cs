using System;
using System.Linq;
using System.Threading.Tasks;
using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Shared;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Infrastructure.Data.Services
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CashbackContext _cashbackContext;

        public CatalogRepository(CashbackContext cashbackContext)
        {
            _cashbackContext = cashbackContext ?? throw new ArgumentNullException(nameof(cashbackContext));
        }

        public bool IsInitializeCatalog()
        {
            return _cashbackContext.Albuns.Any();
        }

        public async Task<IPagedList<Album>> ListAlbums(string genreName, int offset, int limit)
        {
            var q = _cashbackContext.Albuns
                .Include(x=>x.Genre)
                .Where(x => x.Genre.Identifier == genreName)
                .OrderBy(x => x.AlbumName)
                .Take(limit)
                .Skip(offset);

            return new PagedList<Album>(offset, limit, await q.ToListAsync());
        }

        public async Task<bool> Save(Album album)
        {
            await _cashbackContext.Albuns.AddAsync(album);
            return await _cashbackContext.SaveChangesAsync() > 0;
        }

        public async Task<Album> GetByIdAsync(int albumId)
        {
            return await _cashbackContext.Albuns.FirstOrDefaultAsync(x => x.Id == albumId);
        }

        public async Task<double> GetCashbackPercent(int genreId, DayOfWeek dayOfWeek)
        {
            var cashback = await _cashbackContext.GenresCashback
                .FirstOrDefaultAsync(f => f.DayOfWeek == dayOfWeek && f.GenreId == genreId);
            return cashback.Percent;
        }
    }
}

using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Shared;
using Microsoft.AspNetCore.Mvc;
using SpotifyService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ICatalogRepository _catalogRepository;

        public CatalogController(IGenreRepository genreRepository,
                                 ICatalogRepository catalogRepository)
        {
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        [HttpGet]
        [Route("ListCatalog")]
        public async Task<IPagedList<AlbumViewModel>> ListCatalog(string genre, int offset, int limit)
        {
            var albums = await _catalogRepository.ListAlbums(genre, offset, limit);

            return new PagedList<AlbumViewModel>(limit, offset, albums.Items.Select(s => new AlbumViewModel()
            {
                Name = s.AlbumName,
                Identifier = s.Identifier,
                Id = s.Id,
                Price = s.Price,
                GenreId = s.GenreId,
                GenreName = s.Genre.GenreName
            }));
        }

        [HttpGet]
        [Route("ListGenres")]
        public async Task<IEnumerable<GenreViewModel>> ListGenres()
        {
            var genres = await _genreRepository.ListAllGenres();

            return genres
                .Select(x => new GenreViewModel()
                {
                    Id = x.Id,
                    Identifier = x.Identifier,
                    Name = x.GenreName,
                    Icons = new[]
                    {
                        new GenreIconViewModel()
                        {
                            Url = x.Thumbnail
                        }
                    }
                }).ToList();
        }

        [HttpGet]
        [Route("GetAlbumById/{albumId}")]
        public async Task<AlbumViewModel> GetAlbumById(int albumId)
        {
            var album = await _catalogRepository.GetByIdAsync(albumId);
            return new AlbumViewModel()
            {
                Id = album.Id,
                GenreId = album.GenreId,
                Name = album.AlbumName,
                Price = album.Price
            };
        }
    }
}

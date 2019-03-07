using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cashback.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpotifyService.Interfaces;
using System.Linq;
using Cashback.Shared;
using Cashback.Infrastructure.Data.Models;

namespace Cashback.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IGenreRepository _genreRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IConfiguration _configuration;

        public SpotifyController(ISpotifyService spotifyService,
                                 IGenreRepository genreRepository,
                                 ICatalogRepository catalogRepository,
                                 IConfiguration configuration,
                                 IServiceRepository serviceRepository)
        {
            _spotifyService = spotifyService ?? throw new ArgumentNullException(nameof(spotifyService));
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        }

        [HttpGet]
        [Route("IsSyncronized")]
        public ActionResult<bool> IsSyncronized()
        {
            return _catalogRepository.IsInitializeCatalog();
        }

        [HttpGet]
        [Route("SyncronizeData")]
        public async Task<ActionResult<bool>> SyncronizeData()
        {
            var genres = await _spotifyService.ListGenres();
            foreach (var genre in genres)
            {
                await _genreRepository.Save(new Infrastructure.Data.Models.Genre()
                {
                    GenreName = genre.Name,
                    Thumbnail = genre.Icons[0].Url,
                    Identifier = genre.Identifier,
                    Cashback = new List<GenreCashback>()
                    {
                        new GenreCashback()
                        {
                            Percent = GetCashback(DayOfWeek.Friday, genre.Identifier),
                            DayOfWeek = DayOfWeek.Friday
                        },
                        new GenreCashback()
                        {
                            Percent = GetCashback(DayOfWeek.Monday, genre.Identifier),
                            DayOfWeek=  DayOfWeek.Monday
                        },
                        new GenreCashback()
                        {
                            Percent = GetCashback(DayOfWeek.Saturday, genre.Identifier),
                            DayOfWeek=  DayOfWeek.Saturday
                        },
                        new GenreCashback()
                        {
                            Percent = GetCashback(DayOfWeek.Sunday, genre.Identifier),
                            DayOfWeek=  DayOfWeek.Sunday
                        },
                        new GenreCashback()
                        {
                            Percent = GetCashback(DayOfWeek.Thursday, genre.Identifier),
                            DayOfWeek=  DayOfWeek.Thursday
                        },
                        new GenreCashback()
                        {
                            Percent = GetCashback(DayOfWeek.Tuesday, genre.Identifier),
                            DayOfWeek=  DayOfWeek.Friday
                        },
                        new GenreCashback()
                        {
                            Percent = GetCashback(DayOfWeek.Wednesday, genre.Identifier),
                            DayOfWeek=  DayOfWeek.Wednesday
                        },
                    }
                });
            }

            var albuns = await _spotifyService.ListAlbuns();
            foreach (var album in albuns)
            {
                await _catalogRepository.Save(new Infrastructure.Data.Models.Album()
                {
                    AlbumName = album.Name,
                    Identifier = album.Identifier,
                    GenreId = (await _genreRepository.GetByIdentifier(album.GenreName)).Id,
                    Price = album.Price,
                });
            }

            return true;
        }

        Func<DayOfWeek, string, double> GetCashback = (dayOfWeek, genreName) =>
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    if (genreName == "pop") return 15;
                    else if (genreName == "brazilian") return 25;
                    else if (genreName == "classical") return 18;
                    else if (genreName == "rock") return 20;
                    break;
                case DayOfWeek.Monday:
                    if (genreName == "pop") return 7;
                    else if (genreName == "brazilian") return 5;
                    else if (genreName == "classical") return 3;
                    else if (genreName == "rock") return 10;
                    break;
                case DayOfWeek.Saturday:
                    if (genreName == "pop") return 20;
                    else if (genreName == "brazilian") return 30;
                    else if (genreName == "classical") return 25;
                    else if (genreName == "rock") return 40;
                    break;
                case DayOfWeek.Sunday:
                    if (genreName == "pop") return 20;
                    else if (genreName == "brazilian") return 30;
                    else if (genreName == "classical") return 35;
                    else if (genreName == "rock") return 40;
                    break;
                case DayOfWeek.Thursday:
                    if (genreName == "pop") return 10;
                    else if (genreName == "brazilian") return 20;
                    else if (genreName == "classical") return 13;
                    else if (genreName == "rock") return 15;
                    break;
                case DayOfWeek.Tuesday:
                    if (genreName == "pop") return 6;
                    else if (genreName == "brazilian") return 10;
                    else if (genreName == "classical") return 5;
                    else if (genreName == "rock") return 15;
                    break;
                case DayOfWeek.Wednesday:
                    if (genreName == "pop") return 2;
                    else if (genreName == "brazilian") return 15;
                    else if (genreName == "classical") return 8;
                    else if (genreName == "rock") return 15;
                    break;
                default:
                    return 0;
            }
            return 0;
        };


        [HttpGet]
        [Route("RefreshToken")]
        public async Task<AuthorizationInfoViewModel> RefreshToken(string refreshToken)
        {
            var r = await _spotifyService.RefreshAccessToken(refreshToken);

            if (r != null)
            {
                await _serviceRepository.SaveServiceInfo(new Infrastructure.Data.Models.ServiceInfo()
                {
                    AccessToken = r.AccessToken,
                    ExpiresInMinutes = r.ExpiresIn,
                    Name = "Spotify",
                    RefreshToken = r.RefreshToken
                });
            }
            return r;
        }

        [HttpGet]
        [Route("ListAllGenres")]
        public async Task<IEnumerable<GenreViewModel>> ListAllGenres()
        {

            return (await _genreRepository.ListAllGenres())
                    .Select(x => new GenreViewModel()
                    {
                        HRef = x.Thumbnail,
                        Id = x.Id,
                        Name = x.GenreName,
                        Identifier = x.Identifier,
                        Icons = new GenreIconViewModel[]
                        {
                            new GenreIconViewModel()
                            {
                                Url = x.Thumbnail
                            }
                        }
                    })
                    .ToList();
        }
    }
}

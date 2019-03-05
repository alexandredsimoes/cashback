using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cashback.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpotifyService.Interfaces;
using SpotifyService.Models;

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
        public ActionResult SyncronizeData()
        {
            if (!_catalogRepository.IsInitializeCatalog())
                return Redirect(_configuration["Spotify:AuthorizeUrl"]);

            return null;
        }

        [HttpGet]
        [Route("RefreshToken")]
        public async Task<AuthorizationInfoViewModel> RefreshToken(string refreshToken)
        {
            var r = await _spotifyService.RefreshAccessToken(refreshToken);

            if(r != null)
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
            var serviceInfo = _serviceRepository.GetServiceInfo();
            if(serviceInfo != null)
            {
                _spotifyService.AccesToken = serviceInfo.AccessToken;
                _spotifyService.RefreshToken = serviceInfo.RefreshToken;
            }


            var genres = await _spotifyService.ListGenres();
            return genres;
        }
    }
}

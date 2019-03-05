using Cashback.Infrastructure.Data.Interfaces;
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
        private readonly ISpotifyService _spotifyService;
        private readonly IGenreRepository _genreRepository;
        private readonly ICatalogRepository _catalogRepository;

        public CatalogController(ISpotifyService spotifyService, IGenreRepository genreRepository, ICatalogRepository catalogRepository)
        {
            _spotifyService = spotifyService ?? throw new ArgumentNullException(nameof(spotifyService));
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        
    }
}

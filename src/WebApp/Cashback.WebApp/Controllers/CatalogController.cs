using Cashback.Shared;
using Cashback.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.WebApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CatalogController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IActionResult> Genres()
        {
            var httpClient = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);
            //var albums = await httpClient.GetAsync("api/Catalog/ListCatalog?genreName=rock");

            var response = await httpClient.GetStringAsync("api/Catalog/ListGenres");
            var genres = JsonConvert.DeserializeObject<IEnumerable<GenreViewModel>>(response);
            ViewBag.Genres = genres;

            return View();
        }

        public async Task<IActionResult> Index(string genre, int offset, int limit)
        {
            var httpClient = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);
            var response = await httpClient.GetStringAsync($"api/Catalog/ListCatalog?genre={genre}&offset={offset}&limit={limit}");


            var albums = JsonConvert.DeserializeObject<PagedList<AlbumViewModel>>(response);                
            ViewBag.Albums = albums;

            return View();
        }
    }
}

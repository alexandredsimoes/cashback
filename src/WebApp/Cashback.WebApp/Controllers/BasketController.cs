using Cashback.Shared;
using Cashback.Shared.ViewModels;
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
    public class BasketController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BasketController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IActionResult> AddItemToBasket(int albumId, int quantity)
        {
            var httpClient = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);


            var response = await httpClient.GetStringAsync($"api/Catalog/GetAlbumById/{albumId}");
            var album = JsonConvert.DeserializeObject<AlbumViewModel>(response);

            //Adiciona o item ao carrinho
            var r = await httpClient.PostAsJsonAsync("api/Basket/AddItemToBasket", new BasketItemViewModel
            {
                AlbumId = album.Id,
                Quantity = quantity
            });


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);


            var response = await httpClient.GetStringAsync("api/Basket/Items");
            var items = JsonConvert.DeserializeObject<IEnumerable<BasketViewModel>>(response);
            return View(items);
        }
    }
}

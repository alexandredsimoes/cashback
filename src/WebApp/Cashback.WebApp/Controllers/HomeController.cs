using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cashback.WebApp.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Cashback.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IActionResult> Initialize()
        {           
            var client = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);
            var response = await client.GetAsync("api/spotify/issyncronized");
            if (!await response.Content.ReadAsAsync<bool>())
            {
                await client.GetAsync("api/Spotify/SyncronizeData");
            }
            return RedirectToAction("Genres", "Catalog");
        }

        public IActionResult Index()
        {
            //try
            //{
            //    var client = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);
            //    var response = await client.GetAsync("/api/spotify/issyncronized");
            //    if (!await response.Content.ReadAsAsync<bool>())
            //    {

            //    }
            //}
            //catch (Exception ex)
            //{

                
            //}


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

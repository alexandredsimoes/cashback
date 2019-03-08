using Cashback.Shared;
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
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrderController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var httpClient = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);


            var response = await httpClient.GetAsync("api/Orders/PlaceOrder");
            var id = await response.Content.ReadAsAsync<int>();
            return RedirectToAction("Details", new { orderId = id });
        }

        public async Task<IActionResult> Details(int orderId)
        {
            var httpClient = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);


            var response = await httpClient.GetStringAsync($"api/Orders/GetOrder?orderId={orderId}");
            var result = JsonConvert.DeserializeObject<OrderViewModel>(response);
            return View(result);
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient(_configuration["Cashback.App:HttpClientFactory"]);

            var startDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            var endDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
            var response = await httpClient.GetStringAsync($"api/Orders/ListAll?startDate={startDate}&endDate={endDate}&limit=10&offset=0");
            var result = JsonConvert.DeserializeObject<PagedList<OrderViewModel>>(response);
            return View(result);
        }
    }
}

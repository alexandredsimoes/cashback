using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IBasketRepository _basketRepository;

        public BasketController(ICatalogRepository catalogRepository,
                                IBasketRepository basketRepository)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpPost]
        [Route("AddItemToBasket")]
        public async Task<ActionResult> AddItemToBasket([FromBody]BasketItemViewModel basketItemViewModel)
        {
            var album = await _catalogRepository.GetByIdAsync(basketItemViewModel.AlbumId);

            if (album == null) return NotFound();

            return Ok(await _basketRepository.AddItemToBasket(basketItemViewModel.AlbumId, basketItemViewModel.Quantity,
                album.Price));
        }

        [HttpGet]
        [Route("Items")]
        public async Task<IEnumerable<BasketViewModel>> Items()
        {
            var basket = await _basketRepository.ListAll();
            return basket
                .Select(c => new BasketViewModel()
                {
                    AlbumId = c.AlbumId,
                    AlbumName = c.Album.AlbumName,
                    Id = c.Id,
                    Quantity = c.Quantity,
                    TotalPrice = c.Quantity * c.UnitPrice,
                    UnityPrice = c.UnitPrice
                }).ToList();
        }
    }
}

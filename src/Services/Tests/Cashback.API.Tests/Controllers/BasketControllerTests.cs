using Cashback.API.Controllers;
using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cashback.API.Tests.Controllers
{
    public class BasketControllerTests
    {
        private readonly BasketController _basketController;
        private readonly Mock<ICatalogRepository> _catalogRepository;
        private readonly Mock<IBasketRepository> _basketRepository;

        public BasketControllerTests()
        {
            _basketRepository = new Mock<IBasketRepository>();
            _catalogRepository = new Mock<ICatalogRepository>();

            _basketController = new BasketController(_catalogRepository.Object,
                _basketRepository.Object);
        }

        [Fact]
        public async Task MustReturnNotFoundWhenAlbumNotExists()
        {
            _catalogRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Album>(null));

            var fake = new BasketItemViewModel();
            var result = await _basketController.AddItemToBasket(fake);

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task MustCallAddItemToBasket()
        {
            _catalogRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new Album()
                {
                    AlbumName = "The Fake Album",
                    Id = 1,
                    Price = 100
                }));

            var fake = new BasketItemViewModel();
            var result = await _basketController.AddItemToBasket(fake);
            _catalogRepository.Verify(f => f.GetByIdAsync(It.IsAny<int>()));
            _catalogRepository.VerifyNoOtherCalls();
            _basketRepository.Verify(f => f.AddItemToBasket(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<double>()));
            _basketRepository.VerifyNoOtherCalls();
            
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task MustCallItems()
        {
            IEnumerable<Basket> items = new List<Basket>();
            _basketRepository.Setup(s => s.ListAll())
                .Returns(Task.FromResult(items));

            var result = await _basketController.Items();
            _basketRepository.Verify(v => v.ListAll());
            _basketRepository.VerifyNoOtherCalls();
        }
    }
}

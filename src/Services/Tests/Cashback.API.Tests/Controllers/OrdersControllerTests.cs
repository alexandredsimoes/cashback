using Cashback.API.Controllers;
using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Shared;
using Cashback.Shared.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cashback.API.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IBasketRepository> _basketRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<ICatalogRepository> _catalogRepository;
        private readonly OrdersController _ordersController;

        public OrdersControllerTests()
        {
            _basketRepository = new Mock<IBasketRepository>();
            _orderRepository = new Mock<IOrderRepository>();
            _catalogRepository = new Mock<ICatalogRepository>();

            _ordersController = new OrdersController(_basketRepository.Object,
                                                     _orderRepository.Object,
                                                     _catalogRepository.Object);
        }

        [Fact]
        public async Task MustCallPlaceOrder()
        {
            var fakeOrder = new Order()
            {
                Id = 1,
                CreateDate = DateTime.Now,
                CustomerId = 1,
            };

            IEnumerable<Basket> fakeItems = new List<Basket>()
            {
                new Basket()
                {
                    AlbumId = 1,
                    CustomerId = 1,
                    Id = 1,
                    Quantity = 1,
                    UnitPrice = 10
                }
            };

            var fakeAlbum = new Album()
            {
                AlbumName = "Fake album",
                GenreId = 1,
                Identifier = "fake",
                Id = 1,
                Price = 10
            };

            _catalogRepository.Setup(s => s.GetByIdAsync(1))
                .Returns(Task.FromResult(fakeAlbum));

            _basketRepository.Setup(s => s.ListAll())
                .Returns(Task.FromResult(fakeItems));

            _orderRepository.Setup(s => s.CreateOrder(It.IsAny<Order>()))
                .Returns(Task.FromResult(1));

            var result = await _ordersController.PlaceOrder();

            _basketRepository.Verify(v => v.ListAll());
            _catalogRepository.Verify(v => v.GetByIdAsync(1));
            _catalogRepository.Verify(v => v.GetCashbackPercent(fakeAlbum.GenreId, DateTime.Now.DayOfWeek));

            _orderRepository.Verify(v => v.CreateOrder(It.IsAny<Order>()));
            _orderRepository.VerifyNoOtherCalls();

            Assert.IsType<int>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task MustCallGetOrder(int orderId)
        {
            var fakeOrder = new Order()
            {
                CustomerId = 1,
                CreateDate = DateTime.Now,
                Id = 1,

            };

            var fakeOrderViewModel = new OrderViewModel()
            {
                CustomerId = 1,
                CreateDate = DateTime.Now,
                Id = 1,
                TotalCashback = 10,
                TotalQuantity = 1,
                TotalPrice = 1
            };

            _orderRepository.Setup(s => s.GetOrderById(orderId))
                .Returns(Task.FromResult(fakeOrder));

            var result = _ordersController.GetOrder(orderId);
            _orderRepository.Verify(v => v.GetOrderById(It.IsAny<int>()));
            _orderRepository.VerifyNoOtherCalls();

            Assert.IsType<Task<OrderViewModel>>(result);

        }
    }
}

using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Shared;
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
    public class OrdersController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogRepository _catalogRepository;

        public OrdersController(IBasketRepository basketRepository,
                                IOrderRepository orderRepository,
                                ICatalogRepository catalogRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        [HttpGet]
        [Route("PlaceOrder")]
        public async Task<int> PlaceOrder()
        {
            var order = new Order()
            {
                CreateDate = DateTime.Now,
                CustomerId = 1,
            };

            var basket = await _basketRepository.ListAll();
            foreach (var item in basket)
            {
                order.Items.Add(await processOrderItem(item.AlbumId, item.Quantity, item.UnitPrice));
            }

            return await _orderRepository.CreateOrder(order);
        }

        private async Task<OrderItem> processOrderItem(int albumId, int quantity, double unityPrice)
        {
            var album = await _catalogRepository.GetByIdAsync(albumId);
            var percent = await _catalogRepository.GetCashbackPercent(album.GenreId, DateTime.Now.DayOfWeek);
            var result = new OrderItem()
            {
                AlbumId = album.Id,
                Quantity = quantity,
                UnityPrice = unityPrice,
                CashbackPercent = percent,
                CashBackTotal  = (quantity * unityPrice) / percent
            };

            return result;
        }

        [HttpGet]
        [Route("GetOrder")]
        public async Task<OrderViewModel> GetOrder(int orderId)
        {

            var order = await _orderRepository.GetOrderById(orderId);

            return new OrderViewModel()
            {
                CreateDate = order.CreateDate,
                CustomerId = order.CustomerId,
                Customer = order.Customer.Name,
                Id = order.Id,
                TotalCashback = order.Items.Sum(x => x.CashBackTotal),
                TotalPrice = order.Items.Sum(x => x.UnityPrice),
                TotalQuantity = order.Items.Sum(x => x.Quantity),
                Items = order.Items.Select(x => new OrderItemViewModel()
                {
                    AlbumId = x.AlbumId,
                    AlbumName = x.Album.AlbumName,
                    CashbackPercent = x.CashbackPercent,
                    CashBackTotal = x.CashBackTotal,
                    Id = x.Id,
                    OrderId = x.OrderId,
                    Quantity = x.Quantity,
                    UnityPrice = x.UnityPrice
                }).ToList()
            };
        }


        [HttpGet]
        [Route("ListAll")]
        public async Task<IPagedList<OrderViewModel>> ListAll(DateTime startDate, DateTime endDate,
            int offset, int limit)
        {

            var o = await _orderRepository.ListOrders(startDate, endDate, offset, limit);

            return  new PagedList<OrderViewModel>(limit, offset, o.Items.Select(order=>new OrderViewModel()
            {
                CreateDate = order.CreateDate,
                CustomerId = order.CustomerId,
                Customer = order.Customer.Name,
                Id = order.Id,
                TotalCashback = order.Items.Sum(x => x.CashBackTotal),
                TotalPrice = order.Items.Sum(x => x.UnityPrice),
                TotalQuantity = order.Items.Sum(x => x.Quantity),
                Items = order.Items.Select(x => new OrderItemViewModel()
                {
                    AlbumId = x.AlbumId,
                    AlbumName = x.Album.AlbumName,
                    CashbackPercent = x.CashbackPercent,
                    CashBackTotal = x.CashBackTotal,
                    Id = x.Id,
                    OrderId = x.OrderId,
                    Quantity = x.Quantity,
                    UnityPrice = x.UnityPrice
                }).ToList()
            }));
        }
    }
}

using Cashback.API.Interfaces;
using Cashback.API.ViewModels;
using Cashback.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Services
{
    public class OrderService : IOrderService
    {
        public Task<int> CreateOrder(OrderViewModel order)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> ListOrders(OrderFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}

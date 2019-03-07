using Cashback.Infrastructure.Data.Models;
using Cashback.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(Order order);
        Task<IPagedList<Order>> ListOrders(DateTime startDate, DateTime endDate, int offset, int limit);
        Task<Order> GetOrderById(int orderId);
    }
}

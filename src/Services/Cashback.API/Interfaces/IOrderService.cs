using Cashback.API.Models;
using Cashback.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> ListOrders(OrderFilter filter);
        Task<OrderViewModel> GetOrderById(int orderId);
        Task<int> CreateOrder(OrderViewModel order);
    }
}

using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CashbackContext _cashbackContext;

        public OrderRepository(CashbackContext cashbackContext)
        {
            _cashbackContext = cashbackContext ?? throw new ArgumentNullException(nameof(cashbackContext));
        }

        public async Task<int> CreateOrder(Order order)
        {
            await _cashbackContext.Orders.AddAsync(order);

            await _cashbackContext.SaveChangesAsync();
            return order.Id;
        }

        public async Task<IPagedList<Order>> ListOrders(DateTime startDate,
                                                        DateTime endDate,
                                                        int offset,
                                                        int limit)
        {
            var result = await _cashbackContext.Orders
                .Include(c => c.Customer)
                .Include(c => c.Items).ThenInclude(a => a.Album)
                .Where(x => x.CreateDate.Date >= startDate && x.CreateDate.Date <= endDate)
                .OrderBy(x => x.CreateDate)
                .Take(limit)
                .Skip(offset * limit)
                .ToListAsync();

            return new PagedList<Order>(limit, offset, result);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var result = await _cashbackContext.Orders
                .Include(c => c.Customer)
                .Include(c => c.Items).ThenInclude(a => a.Album)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            return result;
        }
    }
}

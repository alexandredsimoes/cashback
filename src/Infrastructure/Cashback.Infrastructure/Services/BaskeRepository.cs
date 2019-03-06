using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Services
{
    public class BaskeRepository : IBasketRepository
    {
        private readonly CashbackContext _cashbackContext;

        public BaskeRepository(CashbackContext cashbackContext)
        {
            _cashbackContext = cashbackContext ?? throw new ArgumentNullException(nameof(cashbackContext));
        }

        public async Task<bool> AddItemToBasket(int albumId, int quantity, double unitPrice)
        {
            await _cashbackContext.Basket.AddAsync(new Basket()
            {
                AlbumId = albumId,
                Quantity = quantity,                
            });
            return await _cashbackContext.SaveChangesAsync() > 0;
        }

        public Task<bool> RemoveItemFromBasket(int albumId)
        {
            throw new NotImplementedException();
        }
    }
}

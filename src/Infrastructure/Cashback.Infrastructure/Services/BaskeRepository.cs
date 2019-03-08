using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Services
{
    public class BasketRepository : IBasketRepository
    {
        private readonly CashbackContext _cashbackContext;

        public BasketRepository(CashbackContext cashbackContext)
        {
            _cashbackContext = cashbackContext ?? throw new ArgumentNullException(nameof(cashbackContext));
        }

        public async Task<bool> AddItemToBasket(int albumId, int quantity, double unitPrice)
        {
            var basket = await _cashbackContext.Basket.FirstOrDefaultAsync(x => x.AlbumId == albumId);

            if (basket == null)
            {
                await _cashbackContext.Basket.AddAsync(new Basket()
                {
                    AlbumId = albumId,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    CustomerId = 1 //Default
                });
            }
            else
            {
                basket.Quantity += 1;
                _cashbackContext.Update(basket);
            }
            return await _cashbackContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Basket>> ListAll()
        {
            return await _cashbackContext.Basket
                .Include(x=>x.Album)
                .ToListAsync();
        }


        public Task<bool> RemoveItemFromBasket(int albumId)
        {
            throw new NotImplementedException();
        }
    }
}

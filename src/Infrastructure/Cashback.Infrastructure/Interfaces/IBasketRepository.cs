using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Interfaces
{
    public interface IBasketRepository
    {
        Task<bool> AddItemToBasket(int albumId, int quantity, double unitPrice);
        Task<bool> RemoveItemFromBasket(int albumId);
    }
}

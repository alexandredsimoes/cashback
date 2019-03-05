using System;
using System.Linq;
using System.Threading.Tasks;
using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;

namespace Cashback.Infrastructure.Data.Services
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CashbackContext _cashbackContext;

        public CatalogRepository(CashbackContext cashbackContext)
        {
            _cashbackContext = cashbackContext ?? throw new ArgumentNullException(nameof(cashbackContext));
        }

        public bool IsInitializeCatalog()
        {
            return _cashbackContext.Albuns.Any();
        }
    }
}

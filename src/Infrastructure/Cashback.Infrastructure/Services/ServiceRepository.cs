using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Services
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly CashbackContext _context;

        public ServiceRepository(CashbackContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ServiceInfo GetServiceInfo()
        {
            var service = _context.Services.FirstOrDefault();
            return service;
        }

        public async Task<bool> SaveServiceInfo(ServiceInfo serviceInfo)
        {
            if (serviceInfo.Id == default(int))
                await _context.Services.AddAsync(serviceInfo);
            else
                _context.Services.Update(serviceInfo);

            return await _context.SaveChangesAsync() > 0;
        }

        public ServiceInfo GetServiceInfo(int serviceId)
        {
            return _context.Services.FirstOrDefault(x => x.Id == serviceId);
        }
    }
}

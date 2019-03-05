using Cashback.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Interfaces
{
    public interface IServiceRepository
    {
        ServiceInfo GetServiceInfo();
        Task<bool> SaveServiceInfo(ServiceInfo serviceInfo);
    }
}

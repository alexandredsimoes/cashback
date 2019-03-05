using Cashback.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Interfaces
{
    public interface IGenreRepository
    {
        Task<bool> PopulateGenres();
        Task<bool> Save(Genre genre);
    }
}

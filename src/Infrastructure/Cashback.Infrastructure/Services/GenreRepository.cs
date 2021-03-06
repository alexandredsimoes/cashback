﻿using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Services
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CashbackContext _cashbackContext;

        public GenreRepository(CashbackContext cashbackContext)
        {
            _cashbackContext = cashbackContext ?? throw new ArgumentNullException(nameof(cashbackContext));            
        }

        public async Task<Genre> GetByIdentifier(string identifier)
        {
            return await _cashbackContext.Genres.FirstOrDefaultAsync(x => x.Identifier.Equals(identifier));
        }

        public async Task<IEnumerable<Genre>> ListAllGenres()
        {
            var genres = await _cashbackContext.Genres.ToListAsync();
            return genres;
        }

        public Task<bool> PopulateGenres()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save(Genre genre)
        {
            var entry = await _cashbackContext.Genres.AddAsync(genre);
            return await _cashbackContext.SaveChangesAsync() > 0;
        }

        //public async Task<bool> PopulateGenres()
        //{
        //    var genres = await _spotifyService.ListGenres();
        //    foreach (var genre in genres)
        //    {
        //        _cashbackContext.Genres.Add(new Genre()
        //        {
        //            GenreName = genre.Name
        //        });

        //    }
        //    return await _cashbackContext.SaveChangesAsync() > 0;
        //}
    }
}

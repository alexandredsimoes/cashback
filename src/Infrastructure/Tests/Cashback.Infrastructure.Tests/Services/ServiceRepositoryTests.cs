using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cashback.Infrastructure.UnityTests.Services
{
    public class ServiceRepositoryTests
    {
        private readonly CashbackContext _context;
        public ServiceRepositoryTests()
        {
            DbContextOptions<CashbackContext> options;
            var builder = new DbContextOptionsBuilder<CashbackContext>();
            builder.UseInMemoryDatabase("cashback");
            options = builder.Options;
            _context  = new CashbackContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

        }        

        [Fact]
        public async Task Can_Save_GenreAsync()
        {
            var fakeGenre = new Genre()
            {
                GenreName = "ROCK",
                Cashback = new List<GenreCashback>()
                {
                    new GenreCashback()
                    {
                        DayOfWeek = DayOfWeek.Friday,
                        Percent = 80
                    }
                }
            };

            var genreRepository = new GenreRepository(_context);

            var result = await genreRepository.Save(fakeGenre);

            Assert.True(result);
        }

        [Fact]
        public async Task Can_Load_ServiceInfo()
        {
            var fakeService = new ServiceInfo()
            {
                AccessToken = "",
                ExpiresInMinutes = 500,
                Name = "TestService",
                RefreshToken = ""
            };

            var repository = new ServiceRepository(_context);
            var r = await repository.SaveServiceInfo(fakeService);
            var service = repository.GetServiceInfo();
            Assert.NotNull(service);
        }
    }
}

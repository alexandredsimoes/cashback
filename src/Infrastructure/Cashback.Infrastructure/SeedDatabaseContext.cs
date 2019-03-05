using Cashback.Infrastructure.Data.Models;
using SpotifyService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data
{
    public class SeedDatabaseContext
    {
        private readonly ISpotifyService _spotifyService;

        public SeedDatabaseContext(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService ?? throw new ArgumentNullException(nameof(spotifyService));
        }

        public async Task SeedAsync(CashbackContext context)
        {
            return;
            if (context.Database.EnsureCreated())
            {
                if (!context.Services.Any())
                {
                    context.Services.Add(new ServiceInfo()
                    {
                        Id = 1,
                        AccessToken = "BQBPnsJHgXfj7lBQBIyNmaxH_Nx-Qc5EfFSKyHYhS3XkXSjpfGLO-L43-RTjBnct_RmRNQv_QUKDrxatzj-a-eFZPviqieonmXA7Z_9rNPG2wdA2YsvHPNBAuGJYIrhwOocvvJgzBerkgHhfTuGHQPQLlOga",
                        ExpiresInMinutes = 0,
                        Name = "Spotify",
                        RefreshToken = "AQCgfa7rr_U6UyrfgFnI-vfJEEWKf70iCCLZFi3uXUUGQiIKIa6FB5lYi5trlCC2M_CPk3vUQSFz-hZ9r848-dWe1gspRU7tHwMJMwp_NXpQ1m7Qr4jyBkHcUrGIM1Z0iZKhDA"
                    });

                    Func<DayOfWeek, string, double> criarCashback = (dayOfWeek, genreName) =>
                    {
                        switch (dayOfWeek)
                        {
                            case DayOfWeek.Friday:
                                if (genreName == "pop") return 15;
                                else if (genreName == "brazilian") return 25;
                                else if (genreName == "classical") return 18;
                                else if (genreName == "rock") return 20;
                                break;
                            case DayOfWeek.Monday:
                                if (genreName == "pop") return 7;
                                else if (genreName == "brazilian") return 5;
                                else if (genreName == "classical") return 3;
                                else if (genreName == "rock") return 10;
                                break;
                            case DayOfWeek.Saturday:
                                if (genreName == "pop") return 20;
                                else if (genreName == "brazilian") return 30;
                                else if (genreName == "classical") return 25;
                                else if (genreName == "rock") return 40;
                                break;
                            case DayOfWeek.Sunday:
                                if (genreName == "pop") return 20;
                                else if (genreName == "brazilian") return 30;
                                else if (genreName == "classical") return 35;
                                else if (genreName == "rock") return 40;
                                break;
                            case DayOfWeek.Thursday:
                                if (genreName == "pop") return 10;
                                else if (genreName == "brazilian") return 20;
                                else if (genreName == "classical") return 13;
                                else if (genreName == "rock") return 15;
                                break;
                            case DayOfWeek.Tuesday:
                                if (genreName == "pop") return 6;
                                else if (genreName == "brazilian") return 10;
                                else if (genreName == "classical") return 5;
                                else if (genreName == "rock") return 15;
                                break;
                            case DayOfWeek.Wednesday:
                                if (genreName == "pop") return 2;
                                else if (genreName == "brazilian") return 15;
                                else if (genreName == "classical") return 8;
                                else if (genreName == "rock") return 15;
                                break;
                            default:
                                return 0;
                        }
                        return 0;
                    };

                    //Load genres, albuns
                    var genres = await _spotifyService.ListGenres();
                    foreach (var genre in genres)
                    {
                        context.Add(new Genre()
                        {
                            GenreName = genre.Name,
                            Cashback = new List<GenreCashback>()
                            {
                                new GenreCashback()
                                {
                                    Percent = criarCashback(DayOfWeek.Friday, genre.Name)
                                },
                                new GenreCashback()
                                {
                                    Percent = criarCashback(DayOfWeek.Monday, genre.Name)
                                },
                                new GenreCashback()
                                {
                                    Percent = criarCashback(DayOfWeek.Saturday, genre.Name)
                                },
                                new GenreCashback()
                                {
                                    Percent = criarCashback(DayOfWeek.Sunday, genre.Name)
                                },
                                new GenreCashback()
                                {
                                    Percent = criarCashback(DayOfWeek.Thursday, genre.Name)
                                },
                                new GenreCashback()
                                {
                                    Percent = criarCashback(DayOfWeek.Tuesday, genre.Name)
                                },
                                new GenreCashback()
                                {
                                    Percent = criarCashback(DayOfWeek.Wednesday, genre.Name)
                                },
                            }
                        });
                    }

                    context.SaveChanges();
                }
            }
            return;
        }
    }
}

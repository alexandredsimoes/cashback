using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyService
{
    public static class IoC
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("spotify", cfg =>
            {
                cfg.BaseAddress = new Uri(configuration["Spotify:BaseUrl"]);
                cfg.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                cfg.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "BQBPnsJHgXfj7lBQBIyNmaxH_Nx-Qc5EfFSKyHYhS3XkXSjpfGLO-L43-RTjBnct_RmRNQv_QUKDrxatzj-a-eFZPviqieonmXA7Z_9rNPG2wdA2YsvHPNBAuGJYIrhwOocvvJgzBerkgHhfTuGHQPQLlOga");
            })
            .ConfigurePrimaryHttpMessageHandler(h => new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip,                
            });


            services.AddScoped<ISpotifyService, SpotifyService.Services.SpotifyService>();
        }
    }
}

﻿using Cashback.Shared;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using SpotifyService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyService.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public string RefreshToken { get; set; }
        public string AccesToken { get; set; }

        public SpotifyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private void Authorize()
        {
            var clientId = _configuration["Spotify:ClientId"];
            var redirectUrl = _configuration["Spotify:RedirectUrl"];
            var url = $@"https://accounts.spotify.com/authorize?client_id={clientId}
                        &response_type=code&redirect_uri={redirectUrl}";
        }
        public async Task<IEnumerable<AlbumViewModel>> ListAlbuns()
        {
            IList<AlbumViewModel> result = new List<AlbumViewModel>();
            HttpClient httpClient = _httpClientFactory.CreateClient("spotify");
            httpClient.DefaultRequestHeaders.Authorization =
                                  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccesToken);


            Func<string, string> getGenre = (url) =>
            {
                if (url.Contains("query=pop"))
                    return "pop";
                else if (url.Contains("query=rock"))
                    return "rock";
                else if (url.Contains("query=classical"))
                    return "classical";
                else
                    return "brazilian";
            };

            foreach (var url in new[]
            {
                "search?query=pop&type=album&market=BR&offset=0&limit=50",
                "search?query=rock&type=album&market=BR&offset=0&limit=50",
                "search?query=brazilian&type=album&market=BR&offset=0&limit=50",
                "search?query=classical&type=album&market=BR&offset=0&limit=50",
            })
            {

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(),
                        new
                        {
                            albums = new
                            {
                                href = string.Empty,
                                items = new[]
                                {
                                    new {
                                    id = string.Empty,
                                    name = string.Empty
                                    }
                                }
                            }
                        });

                    Random prices = new Random();
                    foreach (var item in json.albums.items)
                    {
                        result.Add(new AlbumViewModel()
                        {
                            Name = item.name,
                            Price = prices.Next(10, 100),
                            Identifier = item.id,
                            GenreName = getGenre(json.albums.href),
                        });
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<GenreViewModel>> ListGenres()
        {
            IList<GenreViewModel> result = new List<GenreViewModel>();

            HttpClient httpClient = _httpClientFactory.CreateClient("spotify");

            var retryPolicy = await Policy
            .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized ||
                                                    r.StatusCode == HttpStatusCode.BadRequest)
            .RetryAsync(1, async (exception, retryCount) =>
            {
                var r = await RefreshAccessToken(this.RefreshToken);
                if (r != null)
                {
                    RefreshToken = r.RefreshToken;
                    AccesToken = r.AccessToken;
                }

            })
            .ExecuteAsync(async () =>
            {

                HttpResponseMessage response = null;
                foreach (var url in new[]
                {
                    "browse/categories/pop?country=BR&locale=pt_BR",
                    "browse/categories/brazilian?country=BR&locale=pt_BR",
                    "browse/categories/classical?country=BR&locale=pt_BR",
                    "browse/categories/rock?country=BR&locale=pt_BR"
                })
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccesToken);
                    response = await httpClient.GetAsync(url);
                    var json = JsonConvert.DeserializeObject<GenreViewModel>(await response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode && !result.Any(c => c.Name == json.Name))
                        result.Add(json);
                }

                return response;
            });
            return result;
        }

        
        public async Task<AuthorizationInfoViewModel> RefreshAccessToken(string refreshToken)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("spotify");

            string token = string.IsNullOrWhiteSpace(RefreshToken) ?
                refreshToken : RefreshToken;

            this.RefreshToken = String.IsNullOrWhiteSpace(token) ?
                _configuration["Spotify:RefreshToken"] : token;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","refresh_token"),
                new KeyValuePair<string,string>("refresh_token", this.RefreshToken)
            });

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _configuration["Spotify:AuthorizationToken"]);

            var result = await httpClient.PostAsync(_configuration["Spotify:AccessTokenUrl"], content);
            var json = JsonConvert.DeserializeAnonymousType(await result.Content.ReadAsStringAsync(), new
            {
                access_token = string.Empty,
                token_type = string.Empty,
                scope = string.Empty,
                expires_in = 0,
                refresh_token = string.Empty
            });
            return new AuthorizationInfoViewModel()
            {
                AccessToken = json.access_token,
                RefreshToken = json.refresh_token,
                ExpiresIn = json.expires_in,
                TokenType = json.token_type,
            };
        }
    }
}

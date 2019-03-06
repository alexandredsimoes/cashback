using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using SpotifyService.Interfaces;
using SpotifyService.Models;
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
        public Task<IEnumerable<AlbumViewModel>> ListAlbuns()
        {
            
            var url = "search?query=sertanejo&type=album&market=BR&offset=0&limit=50";
            throw new NotImplementedException();
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
                if(r != null)
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

        //private async Task<IEnumerable<dynamic>> ListGenreApi()
        //{


        //    HttpClient httpClient = _httpClientFactory.CreateClient("spotify");

        //    var pop = JsonConvert.DeserializeAnonymousType(await httpClient.GetStringAsync("browse/categories/pop?country=BR&locale=pt_BR"),
        //        jsonDefinition);
        //    var rock = JsonConvert.DeserializeAnonymousType<dynamic>(await httpClient.GetStringAsync("browse/categories/rock?country=BR&locale=pt_BR"), jsonDefinition);
        //    var classica = JsonConvert.DeserializeAnonymousType<dynamic>(await httpClient.GetStringAsync("browse/categories/classical?country=BR&locale=pt_BR"), jsonDefinition);
        //    var mpb = JsonConvert.DeserializeAnonymousType<dynamic>(await httpClient.GetStringAsync("browse/categories/brazilian?country=BR&locale=pt_BR"), jsonDefinition);

        //    return new[] { pop, rock, classica, mpb };
        //}

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


        //public Task<AuthorizationInfoViewModel> RefreshToken(string refreshToken)
        //{
        //    var clientId = _configuration["Spotify:ClientId"];
        //    var secretId = _configuration["Spotify:ClientSecret"];
        //    var redirectUrl = _configuration["Spotify:RedirectUrl"];

        //    throw new NotImplementedException();
        //}
    }
}

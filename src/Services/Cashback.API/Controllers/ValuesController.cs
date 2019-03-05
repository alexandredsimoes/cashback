using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cashback.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using SpotifyService.Interfaces;

namespace Cashback.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;

        public ValuesController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService ?? throw new ArgumentNullException(nameof(spotifyService));
        }


        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            /*
            var url = "https://accounts.spotify.com/api/token";
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            var content = new FormUrlEncodedContent(new[]
            {

                new KeyValuePair<string,string>("grant_type","authorization_code"),
                new KeyValuePair<string,string>("code","AQDyCu8FU7P0CoHMfFEC5jbsFST1GLKo-uvIGzX5ugyiOfBWOT8wzQ6mlupGpK4HUXi5nBjiLjUiViet6TmyE5jJXX0Dxsk2raw_OslKhWzQcDW2N3RVTtDu7uW9K9nCnxTxg6Rge6lr6nqAiJG8iyo51cbFnBW4et8is5tWe5z_Oxvpf87j25EZIHa0cVjxFqMGFw"),
                new KeyValuePair<string,string>("redirect_uri","https://example.com/callback"),
                new KeyValuePair<string,string>("client_id","f8a94e5e99334dd88fe566b22a4fe1f6"),
                new KeyValuePair<string,string>("client_secret","f46a35f5286042eab49c1f5e8f34ffa7")

            });
            //client.DefaultRequestHeaders.Authorization = 
            //    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "ZjhhOTRlNWU5OTMzNGRkODhmZTU2NmIyMmE0ZmUxZjY6ZjQ2YTM1ZjUyODYwNDJlYWI0OWMxZjVlOGYzNGZmYTc=");
            var r = await client.PostAsync(url, content);
            */
            var genres = await _spotifyService.ListGenres();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

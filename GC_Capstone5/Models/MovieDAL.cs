using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GC_Capstone5.Models
{
    public class MovieDAL
    {
        private readonly string _apiKey;
        private readonly string _apiBearerToken;

        public MovieDAL(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("ApiKeys")["MovieAPI"];
            _apiBearerToken = configuration.GetSection("ApiBearerTokens")["MovieDbBearerToken"];
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiBearerToken);
            return client;
        }

        public async Task<string> GetRawJSON(int movieId)
        {
            var client = GetClient();
            var response = await client.GetAsync($"/3/movie/{movieId}?api_key={_apiKey}");
            var movieJSON = await response.Content.ReadAsStringAsync();
            return movieJSON;
        }
        public async Task<Movie> GetMovie(int movieId)
        {
            var client = GetClient();
            var response = await client.GetAsync($"/3/movie/{movieId}");
//            var response = await client.GetAsync($"/3/movie/{movieId}?api_key={_apiKey}");
            var movieJSON = await response.Content.ReadAsStringAsync();
            Movie movie = JsonSerializer.Deserialize<Movie>(movieJSON);
            return movie;
        }
    }
}

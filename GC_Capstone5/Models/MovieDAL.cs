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
        public MovieConfigurationRoot ApiConfig { get; set; }

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

        public async Task<GenreRoot> GetGenres()
        {
            string resource = $"/3/genre/movie/list";
            var client = GetClient();
            var response = await client.GetAsync(resource);
            var genreJSON = await response.Content.ReadAsStringAsync();
            GenreRoot genreRoot = JsonSerializer.Deserialize<GenreRoot>(genreJSON);
            return genreRoot;
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
            int ignore = await GetApiConfiguration();
            ApplyApiImageWebPath(movie);
            return movie;
        }
        public async Task<SearchRootobject> GetMovieByKeyword(string keyword, string? pageNumber)
        {
            string resource = $"/3/search/movie?query={keyword}";
            if (!(pageNumber is null) && int.Parse(pageNumber) > 0)
            {
                resource += $"&page={pageNumber}";
            }
            var client = GetClient();
            var response = await client.GetAsync(resource);
            var movieJSON = await response.Content.ReadAsStringAsync();
            SearchRootobject searchRootobject = JsonSerializer.Deserialize<SearchRootobject>(movieJSON);
            int ignore = await GetApiConfiguration();

            for (int i = 0; i < searchRootobject.results.Length; i++)
            {
                ApplyApiImageWebPath(searchRootobject.results[i]);
            }
            return searchRootobject;
        }
        #region INTERNAL_METHODS
        private async Task<int> GetApiConfiguration()
        {
            if (this.ApiConfig is null)
            {
                var client = GetClient();
                var response = await client.GetAsync("/3/configuration");
                var configJSON = await response.Content.ReadAsStringAsync();
                this.ApiConfig = JsonSerializer.Deserialize<MovieConfigurationRoot>(configJSON);
            }
            return 0;
        }
        private string GetImageUrl()
        {
            string url = "";
            try
            {
                url = ApiConfig.images.base_url;
                if (ApiConfig.images.poster_sizes.Length > 0)
                {
                    url += ApiConfig.images.poster_sizes[0];
                }
            }
            catch
            {

            }
            return url;
        }
        private string MakeApiUrl(string? resource)
        {
            // don't assign anything if the resource parameter is NULL
            if (resource is null)
            {
                return null;
            }
            return GetImageUrl() + resource;
        }
        private void ApplyApiImageWebPath(Movie movie)
        {
            // Prefix the URI to each image so that the use of this object doesn't have to
            // Don't assign anything if the poster_path is NULL
            movie.poster_path =  MakeApiUrl(movie.poster_path);
        }
        private void ApplyApiImageWebPath(SearchResult movie)
        {
            // Prefix the URI to each image so that the use of this object doesn't have to
            // Don't assign anything if the poster_path is NULL
            movie.poster_path = MakeApiUrl(movie.poster_path);
        }
        #endregion
    }
}

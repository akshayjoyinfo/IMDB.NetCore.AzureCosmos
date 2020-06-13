using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class TheMovieDBAPI : IMovieAPI
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string HOST = string.Empty;
        private string API_KEY = string.Empty;
        private string API_IMAGE_URL = string.Empty;

        public TheMovieDBAPI(IConfiguration configuration)
        {
            _configuration = configuration;
            InitAPIConfig();
        }

        private void InitAPIConfig()
        {
            var apiConfigSection = _configuration.GetSection("TheMovieDBAPI");
            HOST = apiConfigSection.GetSection("Host").Value;
            API_KEY = apiConfigSection.GetSection("APIKey").Value;
            API_IMAGE_URL = apiConfigSection.GetSection("APIImageURL").Value;
        }

        public async Task<APIMovieDBSearchResponse> GetMovieListsByQueryAsync(string query)
        {

            APIMovieDBSearchResponse movieSearchResults = new APIMovieDBSearchResponse();
           
            using(_httpClient = new HttpClient())
            {
                var encodedQueryURI = System.Net.WebUtility.UrlEncode(query);

                var requestURL = string.Format($"{HOST}/search/movie?api_key={API_KEY}&query={encodedQueryURI}");

                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(requestURL);

                var responseString = await _httpClient.GetStringAsync(requestURL);
                movieSearchResults = JsonConvert.DeserializeObject<APIMovieDBSearchResponse>(responseString);
            }
            return movieSearchResults;
        }

        public string GetImageURL(string posterPath)
        {
            if(!string.IsNullOrEmpty(posterPath))
            {
                return string.Format($"{API_IMAGE_URL}{posterPath}");
            }
            else
            {
                return "https://image.shutterstock.com/image-vector/no-image-available-sign-absence-260nw-373243873.jpg";
            }
        }

        public async Task<APIMovieResponseDTO> GetMovieDetailsById(int theMovieDbId)
        {
            APIMovieResponseDTO movieDetail = new APIMovieResponseDTO();
            using (_httpClient = new HttpClient())
            {
                var requestURL = string.Format($"{HOST}/movie/{theMovieDbId}?api_key={API_KEY}");

                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(requestURL);

                var responseString = await _httpClient.GetStringAsync(requestURL);
                movieDetail = JsonConvert.DeserializeObject<APIMovieResponseDTO>(responseString);
            }
            return movieDetail;
        }

        public async Task<APIMovieCreditResponse> GetMovieCredits(int theMovieDbId)
        {
            APIMovieCreditResponse movieDetail = new APIMovieCreditResponse();
            using (_httpClient = new HttpClient())
            {
                var requestURL = string.Format($"{HOST}/movie/{theMovieDbId}/credits?api_key={API_KEY}");

                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(requestURL);

                var responseString = await _httpClient.GetStringAsync(requestURL);
                movieDetail = JsonConvert.DeserializeObject<APIMovieCreditResponse>(responseString);
            }
            return movieDetail;
        }

        public async Task<APIPersonResponse> GetPerson(long theMovieDbPersonId)
        {
            APIPersonResponse movieDetail = new APIPersonResponse();
            using (_httpClient = new HttpClient())
            {
                var requestURL = string.Format($"{HOST}/person/{theMovieDbPersonId}?api_key={API_KEY}");

                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(requestURL);

                var responseString = await _httpClient.GetStringAsync(requestURL);
                movieDetail = JsonConvert.DeserializeObject<APIPersonResponse>(responseString);
            }
            return movieDetail;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helpers;

namespace WebApp.Models
{
    [CosmosCollection(Container ="Movies")]
    public class Movie
    {
        [JsonProperty("budget")]
        public long Budget { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }

        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("themovie_api_id")]
        public long TheMovieDBId { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [JsonProperty("production_countries")]
        public List<ProductionCountry> ProductionCountries { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("revenue")]
        public long Revenue { get; set; }

        [JsonProperty("runtime")]
        public long Runtime { get; set; }

        [JsonProperty("spoken_languages")]
        public List<SpokenLanguage> SpokenLanguages { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
    [CosmosCollection(Container = "Genres")]
    public partial class Genre
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [CosmosCollection(Container = "ProductionCompanies")]
    public partial class ProductionCompany
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    public partial class ProductionCountry
    {
        [JsonProperty("country_code")]
        public string Code { get; set; }

        [JsonProperty("country_name")]
        public string Name { get; set; }
    }

    public partial class SpokenLanguage
    {
        [JsonProperty("language_code")]
        public string Code { get; set; }

        [JsonProperty("language_name")]
        public string Name { get; set; }
    }
}

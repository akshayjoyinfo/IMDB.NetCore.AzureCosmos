# IMDB.NetCore.AzureCosmos
This is sample IMDB Movie Application which try to load the date from  theMovideDB and Appeneded to AZURE Cosmos Database

## Prerequisite Steps

1. Get Azure Free Subscription
2. Create API Key fro TheMovieDb https://www.themoviedb.org/settings/api
3. Create Azure Cosmos Account for Storing the Movies imported from API

## ICosmosService Generic Interfce to perform Azure Cosmos Generic Collection

1. Below is the interface for all Collection that need to perform

    ```
    public interface ICosmosService<T>
        {
            Task<IEnumerable<T>> GetQueryAsync(string query);
            Task<T> GetByIdAsync(string id);
            Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
            Task<T> AddAsync(T item);
            Task UpdateAsync(string id, T item);
            Task DeleteAsync(string id);
        }
    ```

2. Create Model like EF [Table="Sample"] , but here we use CustomAttribute to deal with Table

  ```
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
    ```
  [CosmosCollection(Container ="Movies")]
    public class Movie  
    using This all movies will be write into Collection Movies in given Cosmos Database, similar to Table()
    
3. check CosmosService<T> for the implemenation for interface

4. Register ICosmosService<T> with CosmosService<T>
      ```
      services.AddTransient(typeof(ICosmosService<>), typeof(CosmosService<>));
      ```
5. Use the Model in Service/Controller like ths

  ```
       private readonly ICosmosService<Movie> _movieCosmosHandler;
       private readonly ICosmosService<ProductionCompany> _productionCompanyCosmosHandler;
       private readonly ICosmosService<Genre> _genreCosmosHandler;
        
       public MovieDomainService(ICosmosService<Movie> movieCosmosHandler, ICosmosService<ProductionCompany>       productionCompanyCosmosHandler,ICosmosService<Genre> genreCosmosHandler )
        {
            _movieCosmosHandler = movieCosmosHandler;
            _productionCompanyCosmosHandler = productionCompanyCosmosHandler;
            _genreCosmosHandler = genreCosmosHandler;
            _serviceScopeFactory = serviceScopeFactory;
            _movieAPI = movieAPI;
        }
        
        ...
        ...
        
        var result = await _productionCompanyCosmosHandler.AddAsync(prodCompany);
        var result = await _genreCosmosHandler.AddAsync(genre);
        

  ```






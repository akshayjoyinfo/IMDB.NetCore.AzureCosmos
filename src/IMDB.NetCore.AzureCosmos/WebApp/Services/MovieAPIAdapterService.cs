using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class MovieAPIAdapterService : IMovieAPIAdapterService
    {
        private readonly IMovieAPI _movieAPI;

        public MovieAPIAdapterService(IMovieAPI movieAPI)
        {
            _movieAPI = movieAPI;
        }

        public async Task<MovieViewModel> GetDomainMovieByTheMovieDbAPI(MovieQuery movieQuery)
        {
            var tempMovieResult = await _movieAPI.GetMovieDetailsById(movieQuery.id);
            var tempMovieCredits = await _movieAPI.GetMovieCredits(movieQuery.id);

            var preparedMovie = new Movie()
            {
                TheMovieDBId = movieQuery.id,
                Budget = tempMovieResult.Budget,
                Homepage = tempMovieResult.Homepage,
                ImdbId = tempMovieResult.ImdbId,
                OriginalLanguage = tempMovieResult.OriginalLanguage,
                OriginalTitle = tempMovieResult.OriginalTitle,
                Overview = tempMovieResult.Overview,
                Popularity = tempMovieResult.Popularity,
                PosterPath = _movieAPI.GetImageURL(tempMovieResult.PosterPath),
                ReleaseDate = tempMovieResult.ReleaseDate,
                Revenue = tempMovieResult.Revenue,
                Runtime = tempMovieResult.Runtime,
                Status = tempMovieResult.Status,
                Tagline = tempMovieResult.Tagline,
                Title = tempMovieResult.Title,
                Genres = tempMovieResult.Genres.Select(x => new Genre() { Name = x.Name }).ToList(),
                ProductionCountries = tempMovieResult.ProductionCountries.Select(x => new ProductionCountry() { Code = x.Iso3166_1, Name = x.Name }).ToList(),
                SpokenLanguages = tempMovieResult.SpokenLanguages.Select(x => new SpokenLanguage() { Code = x.Iso639_1, Name = x.Name }).ToList(),
                ProductionCompanies = tempMovieResult.ProductionCompanies.Select(x => new ProductionCompany()
                {
                    
                    LogoPath = x.LogoPath,
                    Name = x.Name,
                    OriginCountry = x.OriginCountry

                }).ToList()
            };

            var preparedMovieCasts = tempMovieCredits.Casts.Select(mov => new Cast
            {
                TheMovideDBPersonId = mov.Id,
                Gender = mov.Gender,
                Name = mov.Name,
                Character = mov.Character,
                ProfilePath = _movieAPI.GetImageURL(mov.ProfilePath)
            }).ToList();

            var preparedMovieCrews = tempMovieCredits.Crews.Select(mov => new Crew
            {
                
                TheMovideDBPersonId = mov.Id,
                Gender = mov.Gender,
                Name = mov.Name,
                Department = mov.Department,
                Job = mov.Job,
                ProfilePath = _movieAPI.GetImageURL(mov.ProfilePath)
            }).ToList();


            return new MovieViewModel() { MovieDetails = preparedMovie ,CastDetails = preparedMovieCasts, CrewDetails = preparedMovieCrews };

        }

        public async Task<IEnumerable<MovieSearchResult>> GetMovieListsByQueryAsync(MovieSearchQuery searchQuery)
        {
            var tempResults = await _movieAPI.GetMovieListsByQueryAsync(searchQuery.query);

            var prepatedResults = tempResults.Results.Select(
                                    item => new MovieSearchResult()
                                    {
                                        Id = item.Id,
                                        OriginalTitle = item.OriginalTitle,
                                        PosterPath = _movieAPI.GetImageURL(item.PosterPath),
                                        ReleaseDate = item.ReleaseDate,
                                        Title = item.OriginalTitle
                                    }).ToList();

            return prepatedResults;
        }

        
    }
}

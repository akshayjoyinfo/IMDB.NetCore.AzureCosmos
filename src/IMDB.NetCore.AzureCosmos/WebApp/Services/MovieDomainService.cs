using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Domain.Interfaces;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class MovieDomainService : IMovieDomainService
    {

        private readonly ICosmosService<Movie> _movieCosmosHandler;
        private readonly ICosmosService<ProductionCompany> _productionCompanyCosmosHandler;
        private readonly ICosmosService<Genre> _genreCosmosHandler;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMovieAPI _movieAPI;

        public MovieDomainService(ICosmosService<Movie> movieCosmosHandler, ICosmosService<ProductionCompany> productionCompanyCosmosHandler,
            ICosmosService<Genre> genreCosmosHandler, IServiceScopeFactory serviceScopeFactory, IMovieAPI movieAPI)
        {
            _movieCosmosHandler = movieCosmosHandler;
            _productionCompanyCosmosHandler = productionCompanyCosmosHandler;
            _genreCosmosHandler = genreCosmosHandler;
            _serviceScopeFactory = serviceScopeFactory;
            _movieAPI = movieAPI;
        }

        public async Task<Movie> AddAsync(MovieViewModel viewModel)
        {
            // save Production Companies and Genres
            var movieResult = new Movie();
            var prodcCompanies = viewModel.MovieDetails.ProductionCompanies;
            var addedProdCompanies = new List<ProductionCompany>();

            foreach(var prodCompany in prodcCompanies)
            {
                var resultifExist = (await _productionCompanyCosmosHandler
                                    .GetQueryAsync($"SELECT * FROM c WHERE c.name='{prodCompany.Name}' AND c.origin_country='{prodCompany.OriginCountry}' "))
                                    .FirstOrDefault();
                if (resultifExist == null)
                {
                    prodCompany.Id = Guid.NewGuid().ToString();
                    var result = await _productionCompanyCosmosHandler.AddAsync(prodCompany);
                    addedProdCompanies.Add(result);
                }
                else
                {
                    addedProdCompanies.Add(resultifExist);
                }
            }

            var genres = viewModel.MovieDetails.Genres;
            var addedGenres = new List<Genre>();

            foreach (var genre in genres)
            {
                var resultifExist = (await _genreCosmosHandler
                                    .GetQueryAsync($"SELECT * FROM c WHERE c.name='{genre.Name}' "))
                                    .FirstOrDefault();
                if (resultifExist == null)
                {
                    genre.Id = Guid.NewGuid().ToString();
                    var result = await _genreCosmosHandler.AddAsync(genre);
                    addedGenres.Add(result);
                }
                else
                {
                    addedGenres.Add(resultifExist);
                }
            }

            var newMovie = viewModel.MovieDetails;
            newMovie.Genres = addedGenres;
            newMovie.ProductionCompanies = addedProdCompanies;

            // checking movie if exists

            movieResult = (await _movieCosmosHandler
                                    .GetQueryAsync($"SELECT * FROM c WHERE c.title='{newMovie.Title}' and c.release_date = '{newMovie.ReleaseDate.ToShortDateString()}' "))
                                    .FirstOrDefault();

            if(movieResult == null)
            {
                newMovie.Id = Guid.NewGuid().ToString();
                movieResult = await _movieCosmosHandler.AddAsync(newMovie);
            }

            return movieResult;
        }

        public async Task AddCasts(MovieViewModel model, Movie movie)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                foreach (var cast in model.CastDetails)
                {
                    //check if the cast person are existing in Persons if yes add that in to CAsts Collection with Movie Id

                    var newPerson = new Person();
                    var newCast = new Cast();

                    var castCosmosHandler = scope.ServiceProvider.GetService<ICosmosService<Cast>>();
                    var movieAPIHandler = scope.ServiceProvider.GetService<IMovieAPI>();

                    newPerson = await AddPerson(cast.TheMovideDBPersonId);

                    newCast = (await castCosmosHandler
                                        .GetQueryAsync($"SELECT * FROM c WHERE c.name='{cast.Name}' and c.movie_id = '{movie.Id}' "))
                                        .FirstOrDefault();

                    if(newCast == null)
                    {
                        newCast = new Cast();
                        newCast.Id = Guid.NewGuid().ToString();
                        newCast.Name = cast.Name;
                        newCast.MovieId = movie.Id;
                        newCast.ProfilePath = newPerson.ProfilePath;
                        newCast.TheMovideDBPersonId = cast.TheMovideDBPersonId;
                        newCast.Character = cast.Character;
                        newCast.Gender = cast.Gender;

                        await castCosmosHandler.AddAsync(newCast);
                    }

                }
            }
            
        }

        public async Task AddCredits(MovieViewModel model, Movie movie)
        {
            /*
             *  Adding Credits runs in backgrouond
             *  1. Add Casts -  Persons, Actors details
             *  2. Add Crews -  Person crewd Details
             * 
             * */


            using(IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var addMovieCreditsTasks = new List<Task>();

                    addMovieCreditsTasks.Add(AddCasts(model, movie));
                    addMovieCreditsTasks.Add(AddCrews(model, movie));

                    await Task.WhenAll(addMovieCreditsTasks);
                }catch(Exception exp)
                {
                    Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$" +
                        "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$" +
                        "Exception Details" +
                        "" +
                        exp);
                }
            }
        }

        public async Task AddCrews(MovieViewModel model, Movie movie)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                foreach (var crew in model.CrewDetails)
                {
                    //check if the cast person are existing in Persons if yes add that in to CAsts Collection with Movie Id

                    var newPerson = new Person();
                    var newCrew = new Crew();

                    var crewCosmosHandler = scope.ServiceProvider.GetService<ICosmosService<Crew>>();
                    var movieAPIHandler = scope.ServiceProvider.GetService<IMovieAPI>();

                    newPerson = await AddPerson(crew.TheMovideDBPersonId);

                    newCrew = (await crewCosmosHandler
                                        .GetQueryAsync($"SELECT * FROM c WHERE c.name='{crew.Name}' and c.movie_id = '{movie.Id}' "))
                                        .FirstOrDefault();

                    if (newCrew == null)
                    {
                        newCrew = new Crew();
                        newCrew.Id = Guid.NewGuid().ToString();
                        newCrew.Name = crew.Name;
                        newCrew.MovieId = movie.Id;
                        newCrew.ProfilePath = newPerson.ProfilePath;
                        newCrew.TheMovideDBPersonId = crew.TheMovideDBPersonId;
                        newCrew.Department = crew.Department;
                        newCrew.Job = crew.Job;
                        newCrew.Gender = crew.Gender;

                        await crewCosmosHandler.AddAsync(newCrew);
                    }

                }
            }
        }

        public async Task<Person> AddPerson(long theMoviePersonId)
        {
            string birthDayConditionCheck = string.Empty;
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var newPerson = new Person();

                    var personCosmosHandler = scope.ServiceProvider.GetService<ICosmosService<Person>>();
                    var movieAPIHandler = scope.ServiceProvider.GetService<IMovieAPI>();

                    var personDetail = await _movieAPI.GetPerson(theMoviePersonId);

                    if(personDetail.Birthday == null)
                    {
                        birthDayConditionCheck = personDetail.Birthday == null ? " " : $" and c.birthday = '{personDetail.Birthday.Value.ToShortDateString()}'";
                    }
                    else
                    {
                        birthDayConditionCheck = personDetail.Birthday == null ? " " : $" and c.birthday = '{personDetail.Birthday.Value.ToShortDateString()}'";
                    }
                    
                    newPerson = (await personCosmosHandler
                                        .GetQueryAsync($"SELECT * FROM c WHERE c.name='{personDetail.Name}' {birthDayConditionCheck}"))
                                        .FirstOrDefault();


                    if (newPerson == null)
                    {
                        newPerson = new Person();
                        // change to Automapper one lien source to destination trasform
                        newPerson.Id = Guid.NewGuid().ToString();
                        newPerson.Name = personDetail.Name;
                        newPerson.Biography = personDetail.Biography;
                        newPerson.Birthday = personDetail.Birthday;
                        newPerson.Deathday = personDetail.Deathday;
                        newPerson.Homepage = personDetail.Homepage;
                        newPerson.Gender = personDetail.Gender;
                        newPerson.PlaceOfBirth = personDetail.PlaceOfBirth;
                        newPerson.ProfilePath = _movieAPI.GetImageURL(personDetail.ProfilePath);

                        await personCosmosHandler.AddAsync(newPerson);
                    }

                    return newPerson;
                }catch(Exception exp)
                {
                    Console.WriteLine("Exceptionat AddPerson" +
                        "------------------------------------" +
                        "------------------------------------" +
                        exp);
                    return default(Person);
                }
            }
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            var movies = (await _movieCosmosHandler.GetQueryAsync($"SELECT * FROM c")).ToList();
            return movies;
        }


    }
}

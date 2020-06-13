using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.Interfaces
{
    public interface IMovieAPI
    {
        Task<APIMovieDBSearchResponse> GetMovieListsByQueryAsync(string query);
        string GetImageURL(string posterPath);
        Task<APIMovieResponseDTO> GetMovieDetailsById(int theMovieDbId);

        Task<APIMovieCreditResponse> GetMovieCredits(int theMovieDbId);

        Task<APIPersonResponse> GetPerson(long theMovieDbPersonId);
    }
}

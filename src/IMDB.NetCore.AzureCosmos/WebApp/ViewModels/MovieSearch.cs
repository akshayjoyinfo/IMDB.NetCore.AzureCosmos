using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class MovieSearch
    {
        [Required]
        [MinLength(2)]
        public string query { get; set; }
        public IEnumerable<MovieSearchResult> Results { get; set; }
    }
}

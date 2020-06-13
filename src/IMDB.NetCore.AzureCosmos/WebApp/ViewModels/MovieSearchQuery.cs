using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class MovieSearchQuery
    {
       
        public string query { get; set; }
    }

    public class MovieQuery
    {
        public int id { get; set; }
    }
}

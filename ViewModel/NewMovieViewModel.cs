using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vidly.Models;

namespace vidly.ViewModel
{
    public class NewMovieViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }
        public Movie Movie{ get; set; }
    }
}
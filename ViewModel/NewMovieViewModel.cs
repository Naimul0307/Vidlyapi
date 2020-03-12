using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyApi.Models;

namespace VidlyApi.ViewModel
{
    public class NewMovieViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Movie Movie { get; set; }
    }
}
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Infrastructure
{
    public static class MovieManagerStaticDb
    {
        public static IList<Movie> Movies { get; set; } = new List<Movie>();
        public static IList<User> Users { get; set; } = new List<User>();
    }
}

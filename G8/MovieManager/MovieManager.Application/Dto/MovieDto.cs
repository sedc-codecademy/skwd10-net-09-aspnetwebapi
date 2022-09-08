using MovieManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Dto
{
    public class MovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public MovieGenre Genre { get; set; }
        public MovieRating Rating { get; set; }
    }
}
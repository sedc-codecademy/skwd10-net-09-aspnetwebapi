﻿using MovieManager.Domain.Enums;
using MovieManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Domain.Models
{
    public class Movie : IEntity
    {
        private Movie()
        {

        }

        public Movie(string title, string description, int year, MovieGenre genre, MovieRating rating, User user)
        {
            Title = title;
            Description = description;
            Year = year;
            Genre = genre;
            Rating = rating;
            User = user;
        }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public MovieGenre Genre { get; set; }
        public MovieRating Rating { get; set; }
        public User User { get; set; }
        public void ValidateOwner(int userId)
        {
            if (User.Id != userId)
                throw new UserIsNotOwnerException("User is not owner!", userId);
        }

    }
}
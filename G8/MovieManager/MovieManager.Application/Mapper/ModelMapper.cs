using AutoMapper;
using MovieManager.Application.Dto;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Mapper
{
    public static class ModelMapper
    {
        public static MapperConfiguration GetConfiguration()
        {
            MapperConfiguration cfg = new MapperConfiguration(x =>
            {
                x.CreateMap<RegisterDto, User>()
                .ForMember(m => m.Id, m => m.Ignore())
                .ForMember(y => y.Movies, y => y.Ignore());

                x.CreateMap<MovieDto, Movie>().ReverseMap();

            });

            return cfg;
        }
    }
}

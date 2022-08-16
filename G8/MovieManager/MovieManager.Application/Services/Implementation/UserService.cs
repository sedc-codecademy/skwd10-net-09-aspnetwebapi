using AutoMapper;
using MovieManager.Application.Dto;
using MovieManager.Application.Mapper;
using MovieManager.Application.Repositories;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> _repository;
        private readonly IMapper mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public void Register(RegisterDto model)
        {
            var user = mapper.Map<User>(model);
            _repository.Create(user);
        }
    }
}

using AutoMapper;
using CoolApi.DTOs;
using CoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<File, FileReadDto>().ReverseMap();


            CreateMap<File, FileToAddDto>().ReverseMap();
            CreateMap<User, UserToAddDto>().ReverseMap();
        }
    }
}

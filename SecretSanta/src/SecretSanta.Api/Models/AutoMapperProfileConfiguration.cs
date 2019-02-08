using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserInputViewModel, User>();

            CreateMap<GroupInputViewModel, GroupViewModel>();
            CreateMap<Group, GroupViewModel>();
            CreateMap<GroupInputViewModel, Group>();

            CreateMap<Gift, GiftViewModel>();
        }
    }
}

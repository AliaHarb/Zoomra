using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomra.Domain.DTOS;
using Zoomra.Domain.Entities;

namespace Zoomra.Application.Profiles
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDto,ApplicationUser>().ReverseMap();
        }
    }
}

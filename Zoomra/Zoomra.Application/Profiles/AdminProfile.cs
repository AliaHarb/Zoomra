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
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AddHospitalDto, Hospital>();
            CreateMap<Hospital, AdminHospitalDto>();
            CreateMap<AddRewardDto, Reward>();
        }
    }
}

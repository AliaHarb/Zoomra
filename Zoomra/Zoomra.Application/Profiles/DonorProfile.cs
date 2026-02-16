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
    public class DonorProfile : Profile
    {
        public DonorProfile()
        {
            CreateMap<DonationCenter, DonationCenterDto>();
            CreateMap<EmergencyCall, EmergencyCallDto>();
            CreateMap<Reward, RewardDto>();

            CreateMap<Donation, DonationHistoryDto>();
                
    }
    }
}

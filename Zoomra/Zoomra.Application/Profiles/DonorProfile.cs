using AutoMapper;
using Zoomra.Domain.DTOS;
using Zoomra.Domain.Entities;

namespace Zoomra.Application.Profiles
{
    public class DonorProfile : Profile
    {
        public DonorProfile()
        {
           
            CreateMap<Hospital, DonationCenterDto>();

           
            CreateMap<EmergencyRequest, EmergencyCallDto>();

            CreateMap<Reward, RewardDto>();
            CreateMap<Donation, DonationHistoryDto>();
        }
    }
}
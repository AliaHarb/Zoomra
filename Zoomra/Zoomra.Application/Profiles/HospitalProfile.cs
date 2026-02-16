using AutoMapper;
using Zoomra.Domain.DTOS;
using Zoomra.Domain.Entities;

namespace Zoomra.Application.Profiles
{
    public class HospitalProfile : Profile
    {
        public HospitalProfile()
        {
         
            CreateMap<BloodInventory, BloodStockDto>();

            CreateMap<CreateEmergencyCallDto, EmergencyRequest>()
                .ForMember(dest => dest.BloodTypeNeeded, opt => opt.MapFrom(src => src.BloodType));
            CreateMap<CreateEmergencyCallDto, EmergencyRequest>()
    .ForMember(dest => dest.BloodTypeNeeded, opt => opt.MapFrom(src => src.BloodType))
    .ForMember(dest => dest.UnitsNeeded, opt => opt.MapFrom(src => 0)); 
        }
    }
}
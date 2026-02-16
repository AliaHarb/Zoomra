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
           public class NotificationProfile : Profile
        {
            public NotificationProfile()
            {
                CreateMap<Notification, NotificationDto>()
                    .ForMember(dest => dest.IsEmergency, opt => opt.MapFrom(src => src.Type == "Emergency"));
            }
    }
}

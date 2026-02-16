using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomra.Application.Helper;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Interfaces
{
    public interface IEmergencyService
    {
        Task<Result<bool>> RaiseEmergencyAsync(CreateEmergencyCallDto dto);
    }
}

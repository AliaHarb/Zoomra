using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomra.Application.Helper;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<Result<bool>> UpdateStockAsync(UpdateInventoryDto dto);
        Task<Result<IEnumerable<BloodStockDto>>> GetHospitalInventoryAsync(int hospitalId);

        Task<Result<bool>> ConfirmDonationAsync(ConfirmDonationDto dto);
    }
}

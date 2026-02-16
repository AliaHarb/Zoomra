using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class BloodStockDto
    {
        public string BloodType { get; set; } = null!;
        public int CurrentUnitsCount { get; set; }
    }
}

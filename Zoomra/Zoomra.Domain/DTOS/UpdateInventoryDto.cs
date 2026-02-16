using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class UpdateInventoryDto
    {
        public int HospitalId { get; set; } 
        public string BloodType { get; set; } = null!;
        public int UnitsCount { get; set; }
        public string TransactionType { get; set; } = null!; // "Collected" (إضافة) أو "Used" (سحب)
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
   public class DonorDashboardDto
    {
        
            public string FullName { get; set; } = null!;
            public string BloodType { get; set; } = null!;
            public int Points { get; set; }
            public int UnitsDonated { get; set; } // مقابلة لـ 8 Units في الـ UI
            public int HelpedLives { get; set; }  // مقابلة لـ 3 Helped Lives في الـ UI
            public string NextDonationDate { get; set; } = null!; // مقابلة لـ 10 Days في الـ UI

        

    }
}

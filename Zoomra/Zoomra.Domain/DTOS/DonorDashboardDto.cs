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
            public int UnitsDonated { get; set; } 
            public int HelpedLives { get; set; }  
            public string NextDonationDate { get; set; } = null!; 

        

    }
}

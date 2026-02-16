using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class UserRewardsSummaryDto
    {
    
            public int CurrentPoints { get; set; }
            public string CurrentBadge { get; set; } = null!; // Silver Donor
            public int NextLevelPoints { get; set; } // عشان الـ Progress Bar
            public List<BadgeDto> Badges { get; set; } = new();
        
    }
}

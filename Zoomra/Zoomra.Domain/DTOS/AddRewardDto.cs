using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class AddRewardDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PointsCost { get; set; }
        public string SponsorName { get; set; } = null!;
    }
}

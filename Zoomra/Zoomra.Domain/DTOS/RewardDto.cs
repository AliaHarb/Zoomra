using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class RewardDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!; // Yanfaa Design Course
        public string Description { get; set; } = null!;
        public int PointsRequired { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
    }
}

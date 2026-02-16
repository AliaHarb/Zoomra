using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class BadgeDto
    {
        public string Name { get; set; } = null!;
        public int PointsRequired { get; set; }
        public bool IsUnlocked { get; set; }
    }
}

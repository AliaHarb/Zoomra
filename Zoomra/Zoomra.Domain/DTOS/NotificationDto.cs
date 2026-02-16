using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string CreatedAtFormatted { get; set; }
        public bool IsEmergency { get; set; }
        public string TimeAgo { get; set; } = null!;
    }
}

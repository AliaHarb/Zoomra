using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.Entities
{ 
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public int ImpactLevel { get; set; }
    }
}

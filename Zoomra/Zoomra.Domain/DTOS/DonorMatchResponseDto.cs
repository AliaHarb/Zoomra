using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class DonorMatchResponseDto
    {
        public string Status { get; set; }
        public List<MatchedDonorDto> MatchedDonors { get; set; }
        public int TotalMatches { get; set; }
    }
}

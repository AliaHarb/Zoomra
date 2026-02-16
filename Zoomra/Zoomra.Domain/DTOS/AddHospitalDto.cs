using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class AddHospitalDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; } = null!;

        // الإحداثيات عشان الخريطة والـ AI يحسب المسافات بينها وبين المتبرعين
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string? City { get; set; }

        [Phone]
        public string? ContactPhone { get; set; }

       
        [Required]
        public string AdminId { get; set; } = null!;
    }
}

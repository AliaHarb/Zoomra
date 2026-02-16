using System.ComponentModel.DataAnnotations;

namespace Zoomra.Domain.DTOS
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        
        public int age { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be exactly 14 characters")]
        public string NationalId { get; set; } = null!;

        [Required]
        public string BloodType { get; set; } = null!; // A+, O-, B+, AB+, etc.

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
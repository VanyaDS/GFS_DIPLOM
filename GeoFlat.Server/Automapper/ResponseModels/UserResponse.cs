using System.ComponentModel.DataAnnotations;

namespace GeoFlat.Server.Automapper.ResponseModels
{
    public class UserResponse
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }

        [MaxLength(50)]
        [Required]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required]
        public string Role { get; set; }
    }
}

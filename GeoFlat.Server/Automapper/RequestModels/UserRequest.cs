using System.ComponentModel.DataAnnotations;

namespace GeoFlat.Server.Automapper.RequestModels
{
    public class UserRequest
    {
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
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MaxLength(250)]
        [Required]
        public string Password { get; set; }
    }
}

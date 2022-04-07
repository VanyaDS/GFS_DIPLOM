using System.ComponentModel.DataAnnotations;

namespace GeoFlat.Server.Automapper.RequestModels
{
    public class UserRequestUpdate
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace GeoFlat.Server.Automapper.ResponseModels
{
    public class InfoMessagesResponse
    {
        [Required]
        public bool HasUnreadMessages { get; set; }

        [Required]
        public int NumberOfUnreadMessagesWithUsers { get; set; }
    }
}

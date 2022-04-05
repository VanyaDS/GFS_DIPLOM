using System;
using System.ComponentModel.DataAnnotations;

namespace GeoFlat.Server.Automapper.ResponseModels
{
    public class MessageResponse
    {
        public int messageId { get; set; }
        public int RecipientId { get; set; }
        public int SenderId { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string MessageText { get; set; }
      
        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime SendingDate { get; set; }
        
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
    }
}

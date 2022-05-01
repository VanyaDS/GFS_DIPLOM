using System;
using System.ComponentModel.DataAnnotations;

namespace GeoFlat.Server.Automapper.ResponseModels
{
    public class MessageResponse
    {
        public int messageId { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string MessageText { get; set; }
      
        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime SendingDate { get; set; }


        public int SenderId { get; set; }
        [Required]
        public string SenderName { get; set; }

        [MaxLength(50)]
        [Required]
        public string SenderSurname { get; set; }

        [MaxLength(50)]
        [Required]
        public string SenderPhoneNumber { get; set; }
            
        [MaxLength(100)]
        [Required]
        public string SenderEmail { get; set; }

        [MaxLength(50)]
        [Required]
        public string SenderRole { get; set; }

        public int RecipientId { get; set; }

        public string RecipientName { get; set; }

        [MaxLength(50)]
        [Required]
        public string RecipientSurname { get; set; }

        [MaxLength(50)]
        [Required]
        public string RecipientPhoneNumber { get; set; }
       
        [MaxLength(100)]
        [Required]
        public string RecipientEmail { get; set; }
       
        [MaxLength(50)]
        [Required]
        public string RecipientRole { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoFlat.Server.Models.Database.Entities
{
    [Table("Message")]
    public class Message
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("message_text")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string MessageText { get; set; }

        [Column("sending_date")]
        [Required]
        public DateTime SendingDate { get; set; }

        [Column("is_read")]
        [Required]
        public bool IsRead { get; set; }

        [Column("sender")]
        public int? Sender { get; set; }

        [Column("recipient")]
        public int? Recipient { get; set; }
        public User UserSender { get; set; }
        public User UserRecipient { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoFlat.Server.Models
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
       
        [Column("sender")]
        public int? Sender { get; set; }
      
        [Column("recipient")]
        public int? Recipient { get; set; }
        public User UserSender { get; set; }
        public User UserRecipient { get; set; }
    }
}

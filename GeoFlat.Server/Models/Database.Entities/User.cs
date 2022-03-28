using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoFlat.Server.Models.Database.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Column("surname")]
        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }


        [Column("phone_number")]
        [MaxLength(50)]
        [Required]
        public string PhoneNumber { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public List<Record> Records { get; set; } = new List<Record>();
        public List<Message> SentMessages { get; set; } = new List<Message>();
        public List<Message> ReceviedMessages { get; set; } = new List<Message>();
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
        public List<Comparison> Comparisons { get; set; } = new List<Comparison>();
    }
}

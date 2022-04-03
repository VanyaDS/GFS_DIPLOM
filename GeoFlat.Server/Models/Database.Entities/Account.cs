using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoFlat.Server.Models.Database.Entities
{
    [Table("Account")]
    [Index("Email", IsUnique = true, Name = "Email_Index")]
    public class Account
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("email")]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [Column("password")]
        [MaxLength(250)]
        [Required]
        public string Password { get; set; }

        [Column("role")]
        [MaxLength(50)]
        [Required]
        public string Role { get; set; }
        public User User { get; set; }
    }
}

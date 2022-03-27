using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoFlat.Server.Models
{
    [Table("Flat")]
    public class Flat
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("room_number")]
        [Required]
        public int RoomNumber { get; set; }

        [Column("area")]
        [Required]
        public double Area { get; set; }

        [Column("floor")]
        [Required]
        public int Floor { get; set; }

        public int GeolocationId { get; set; }

        public Geolocation Geolocation { get; set; }

        public Record Record { get; set; }

    }
}

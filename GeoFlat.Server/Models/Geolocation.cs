using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GeoFlat.Server.Models
{
    [Table("Geolocation")]
    public class Geolocation
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("city_name")]
        [MaxLength(100)]
        [Required]
        public string CityName { get; set; }

        [Column("street_name")]
        [MaxLength(100)]
        [Required]
        public string StreetName { get; set; }
       
        [Column("house_number")]
        [MaxLength(20)]
        [Required]
        public string HouseNumber { get; set; }
        public List<Flat> Flats { get; set; } = new List<Flat>();
    }
}

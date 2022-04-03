using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoFlat.Server.Models.Database.Entities
{
    [Table("Record")]
    public class Record
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("pictures_path")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string PicturesPath { get; set; }

        [Column("description")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        [Column("publication_date")]
        [Required]
        public DateTime PublicationDate { get; set; }

        [Column("record_title")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string RecordTitle { get; set; }
       
        [Column("price")]
        [Required]
        public int Price { get; set; }       

        [Column("rent_type")]
        [Required]
        public bool RentType { get; set; }
      
        [Column("rent_status")]
        [Required]
        public bool RentStatus { get; set; }
       
        [Column("has_furniture")]
        [Required]
        public bool HasFurniture { get; set; }
       
        [Column("not_for_students")]
        [Required]
        public bool NotForStudents { get; set; }
       
        [Column("without_animals")]
        [Required]
        public bool WithoutAnimals { get; set; }
       
        [Column("without_children")]
        [Required]
        public bool WithoutChildren { get; set; }
       
        [Column("is_agent")]
        [Required]
        public bool IsAgent { get; set; }
       
        [Column("with_internet")]
        [Required]
        public bool WithInternet { get; set; }
       
        [Column("for_day")]
        [Required]
        public bool ForDay { get; set; }

        public int FlatId { get; set; }
        public Flat Flat { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
        public List<Comparison> Comparisons { get; set; } = new List<Comparison>();
    }
}

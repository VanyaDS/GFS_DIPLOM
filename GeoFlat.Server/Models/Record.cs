using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoFlat.Server.Models
{
    [Table("Record")]
    public class Record
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("pictures_path")]
        [MaxLength(250)]
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
        
        [Column("room_number")]
        [Required]
        public int RoomNumber { get; set; }

        [Column("rent_type")]
        public bool RentType { get; set; }
      
        [Column("rent_status")]
        public bool RentStatus { get; set; }
       
        [Column("has_furniture")]
        public bool HasFurniture { get; set; }
       
        [Column("not_for_students")]
        public bool NotForStudents { get; set; }
       
        [Column("without_animals")]
        public bool WithoutAnimals { get; set; }
       
        [Column("without_children")]
        public bool WithoutChildren { get; set; }
       
        [Column("is_agent")]
        public bool IsAgent { get; set; }
       
        [Column("with_internet")]
        public bool WithInternet { get; set; }
       
        [Column("for_day")]
        public bool ForDay { get; set; }

        public int FlatId { get; set; }
        public Flat Flat { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace GeoFlat.Server.Automapper.ResponseModels
{
    public class ComparisonResponse
    {
        public int Id { get; set; }

        [Required]
        public int RecordId { get; set; }

        [Required]
        public int UserId { get; set; }

        [MaxLength(250)]
        [Required]
        public string PicturesPath { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string RecordTitle { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public bool RentType { get; set; }

        [Required]
        public bool RentStatus { get; set; }

        [Required]
        public bool HasFurniture { get; set; }

        [Required]
        public bool NotForStudents { get; set; }

        [Required]
        public bool WithoutAnimals { get; set; }

        [Required]
        public bool WithoutChildren { get; set; }

        [Required]
        public bool IsAgent { get; set; }

        [Required]
        public bool WithInternet { get; set; }

        [Required]
        public bool ForDay { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public int Floor { get; set; }

        [MaxLength(100)]
        [Required]
        public string CityName { get; set; }

        [MaxLength(100)]
        [Required]
        public string StreetName { get; set; }

        [MaxLength(20)]
        [Required]
        public string HouseNumber { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoFlat.Server.Models.Database.Entities
{
    public class RecordRequestModel
    {
        [MaxLength(100)]
        [Required]
        public string CityName { get; set; }


        [MaxLength(100)]
        [Required]
        public string StreetName { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public int Floor { get; set; }
        [MaxLength(250)]
        [Required]
        public string PicturePath { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }


        [DataType(DataType.MultilineText)]
        [Required]
        public string RecordTitle { get; set; }

        [Required]
        public int Price { get; set; }
        public bool RentType { get; set; }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoFlat.Server.Models.Database.Entities
{
    [Table("Favorites")]
    public class Favorite
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }     
        public int? UserId { get; set; }
        public int? RecordId { get; set; }
        public User User { get; set; }
        public Record Record { get; set; }        
    }
}
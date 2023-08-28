using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthHub2.Models
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; } 

        [Required]
        public int PatientId { get; set; } 

        [Required]
        public int DoctorId { get; set; } 

        [Required]
        [Range(1, 5)] // Rating can only be an integer between 1 and 5
        public int Score { get; set; }

        [Required]
        public DateTime RatingDate { get; set; }

        // Foreign Key relationships
        [ForeignKey("PatientId")] 
        public virtual Patient Patient { get; set; }

        [ForeignKey("DoctorId")] 
        public virtual Doctor Doctor { get; set; }
    }
}
using iTextSharp.text.pdf.parser;
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

        
        public string PatientId { get; set; }

        [Required]
        public string DoctorId { get; set; }

        [Required]
        [Range(1, 5)] // Rating can only be an integer between 1 and 5
        public int Score { get; set; }

        [Required]
        public DateTime RatingDate { get; set; }
    }
}
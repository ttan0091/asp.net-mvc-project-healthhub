using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthHub2.Models
{
    public class MedicalImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime CaptureDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        // Foreign Key relationships
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("ServiceId")]
        public virtual ServiceType ServiceType { get; set; }
    }
}
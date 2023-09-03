using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace HealthHub2.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required]
        [ForeignKey("GeoLocation")]
        public int LocationId { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Required]
        //[Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public string Note { get; set; }
        public string Gender { get; set; }

        // Navigation Properties for Foreign Key relationships
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual GeoLocation GeoLocation { get; set; }
    }
}
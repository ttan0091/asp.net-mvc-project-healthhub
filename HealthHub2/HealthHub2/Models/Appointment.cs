using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HealthHub2.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        //[Required]
        //[ForeignKey("PatientUser")]
        public string PatientId { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        //[Required]
        //[ForeignKey("DoctorUser")]
        public string DoctorId { get; set; }

        [Required]
        [ForeignKey("GeoLocation")]
        [Display(Name = "Clinic Name")]
        public int LocationId { get; set; }

        [Required]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }

        [Required]
        //[Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public string Note { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime? UploadDate { get; set; }

        // Navigation Properties for Foreign Key relationships
        //[ForeignKey("PatientId")]
        //public virtual ApplicationUser PatientUser { get; set; } 

        //[ForeignKey("DoctorId")]
        //public virtual ApplicationUser DoctorUser { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }
    }
}

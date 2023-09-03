using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace HealthHub2.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required] 
        [EmailAddress]
        [StringLength(256)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        [StringLength(50)]
        [DefaultValue("doctor")]
        public string Status { get; set; }

        
    }
}
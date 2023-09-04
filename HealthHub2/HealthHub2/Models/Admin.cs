using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthHub2.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthHub2.Models
{
    public class DoctorRateViewModel
    {
        public string DoctorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public double Score { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthHub2.Models
{
    public class AppointmentViewModel
    {
        public string PatientName { get; set; }
        public string ServiceType { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? UploadDate { get; set; }
        public string DoctorName { get; set; }
        public string ClinicName { get; set; }
    }

}
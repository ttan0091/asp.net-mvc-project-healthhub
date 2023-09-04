using System.ComponentModel;

namespace HealthHub2.ViewModels
{
    public class MedicalImageInfoViewModel
    {


        [DisplayName("Doctor Name")]
        public string DoctorFullName { get; set; }

        [DisplayName("Service Type")]
        public string ServiceType { get; set; }

        [DisplayName("Clinic Name")]
        public string PlaceName { get; set; }

        [DisplayName("Medical Image")]
        public string ImageUrl { get; set; }

        [DisplayName("Appointment Date")]
        public string AppointmentDate { get; set; }

        [DisplayName("Upload Date")]
        public string UploadDate { get; set; }

        [DisplayName("Note")]
        public string Note { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Patient Name")]
        public string PatientFullName { get; set; }


    }
}

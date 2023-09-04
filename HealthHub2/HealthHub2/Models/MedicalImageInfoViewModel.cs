using System.ComponentModel;

namespace HealthHub2.ViewModels
{
    public class MedicalImageInfoViewModel
    {
        [DisplayName("Capture Date")]
        public string CaptureDate { get; set; }

        [DisplayName("Doctor Name")]
        public string DoctorFullName { get; set; }

        [DisplayName("Service Type")]
        public string ServiceType { get; set; }

        [DisplayName("Clinic Name")]
        public string PlaceName { get; set; }

        [DisplayName("Medical Image")]
        public string ImageUrl { get; set; }
    }
}

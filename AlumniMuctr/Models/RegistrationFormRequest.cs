using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class RegistrationFormRequest
    {
        public string FCs { get; set; }
        [Required]
        public string FCsгUniversity { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Faculty { get; set; }
        public string? ScientificSupervisor { get; set; }
        [Required]
        public DateTime EndUniversityTime { get; set; }
        public string? CurrentLivingPlace { get; set; }
        public string? CurrentWorkingPlace { get; set; }
        public string? CurrentPosition { get; set; }
        public string? SignificantAchievements { get; set; }
        public string? GraduatesOfMUCTRMHTI { get; set; }
        public string? Hobby { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool Subscription { get; set; }
        public bool LiveOfAssociation { get; set; }
        public bool FunSaturday { get; set; }
        public bool DataProcessing { get; set; }

        public RegistrationFormRequest()
        {

        }
    }
}

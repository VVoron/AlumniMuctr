using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class RegistrationForm
    { 
        [Key]
        public int Id { get; set; } 
        [Required]
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
        public string? Photo { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool Subscription { get; set; }
        public bool LiveOfAssociation { get; set; }
        public bool FunSaturday { get; set; }
        public bool DataProcessing { get; set; }
        public DateTime? TimeRegistration { get; set; } = DateTime.Now;

        public RegistrationForm()
        {

        }

        public RegistrationForm(RegistrationFormRequest request)
        {
            FCs = request.FCs;
            FCsгUniversity = request.FCsгUniversity;
            Gender = request.Gender;
            Birthday = request.Birthday;
            Faculty = request.Faculty;
            ScientificSupervisor = request.ScientificSupervisor;
            EndUniversityTime = request.EndUniversityTime;
            CurrentLivingPlace = request.CurrentLivingPlace;
            CurrentWorkingPlace = request.CurrentWorkingPlace;
            CurrentPosition = request.CurrentPosition;
            SignificantAchievements = request.SignificantAchievements;
            GraduatesOfMUCTRMHTI = request.GraduatesOfMUCTRMHTI;
            Hobby = request.Hobby;
            Email = request.Email;
            Phone = request.Phone;
            Subscription = request.Subscription;
            LiveOfAssociation = request.LiveOfAssociation;
            FunSaturday = request.FunSaturday;
            DataProcessing = request.DataProcessing;
            //Photo = request.Photo != null ? "wwwroot\\media\\UserPictures\\" + request.Photo.FileName : null;
        }
    }
}

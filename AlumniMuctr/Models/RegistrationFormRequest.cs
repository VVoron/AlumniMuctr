using AlumniMuctr.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class RegistrationFormRequest
    {
        public Guid Id { get; set; }
        [FullName(ErrorMessage = "Введите фамилию, имя и отчество (при наличии)")]
        public string FCs { get; set; }
        public string FCsгUniversity { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Faculty { get; set; }
        public int EndUniversityTime { get; set; }
        public string? CurrentWorkingPlace { get; set; }
        public string? CurrentPosition { get; set; }
        public string? SignificantAchievements { get; set; }
        public string? GraduatesOfMUCTRMHTI { get; set; }
        public string? Hobby { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool Subscription { get; set; }
        public bool LiveOfAssociation { get; set; }
        public bool FunSaturday { get; set; }
        public bool DataProcessing { get; set; }
        public bool IsVerified { get; set; }

        public RegistrationFormRequest()
        {

        }

        public RegistrationFormRequest(RegistrationForm request)
        {
            Id = request.Id;
            FCs = request.FCs;
            FCsгUniversity = request.FCsгUniversity;
            Birthday = request.Birthday;
            Faculty = request.Faculty;
            EndUniversityTime = request.EndUniversityTime.Year;
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
            PhotoUrl = request.Photo;
            IsVerified = request.IsVerified;
        }

        public static implicit operator RegistrationFormRequest(RegistrationForm entity)
        {
            return new RegistrationFormRequest(entity);
        }
    }
}

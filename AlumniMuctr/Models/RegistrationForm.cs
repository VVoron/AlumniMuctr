using DocumentFormat.OpenXml.Office2010.Drawing;
using Org.BouncyCastle.Crypto;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AlumniMuctr.Models
{
    public class RegistrationForm
    { 
        [Key]
        public Guid Id { get; set; } 
        public string FCs { get; set; }
        public string FCsгUniversity { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Faculty { get; set; }
        public DateTime EndUniversityTime { get; set; }
        public string? CurrentWorkingPlace { get; set; }
        public string? CurrentPosition { get; set; }
        public string? SignificantAchievements { get; set; }
        public string? GraduatesOfMUCTRMHTI { get; set; }
        public string? Hobby { get; set; }
        public string? Photo { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool Subscription { get; set; } = false;
        public bool LiveOfAssociation { get; set; } = false;
        public bool FunSaturday { get; set; } = false;
        public bool DataProcessing { get; set; }= false;
        public DateTime? TimeRegistration { get; set; } = DateTime.Now;
        public bool IsVerified { get; set; } = false;

        public RegistrationForm()
        {

        }

        public RegistrationForm(RegistrationFormRequest request)
        {
            Id = request.Id;
            FCs = request.FCs;
            FCsгUniversity = request.FCsгUniversity;
            Birthday = request.Birthday;
            Faculty = request.Faculty;
            EndUniversityTime = DateTime.ParseExact(
                    request.EndUniversityTime.ToString(),
                    "yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None
                );
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
            Photo = request.PhotoUrl;
            IsVerified = request.IsVerified;
        }
        public string[] GetInfoForTable()
        {
            return new string[]
            {
                Id.ToString(),
                (IsVerified) ? "+" : "-",
                FCs,
                FCsгUniversity,
                Birthday.ToString(),
                Faculty,
                EndUniversityTime.ToString(),
                CurrentWorkingPlace,
                CurrentPosition,
                SignificantAchievements,
                GraduatesOfMUCTRMHTI,
                Hobby,
                Photo,
                Email,
                Phone,
                (Subscription) ? "да" : "нет",
                (LiveOfAssociation) ? "+" : "-",
                (FunSaturday) ? "+" : "-",
                (DataProcessing) ? "+" : "-"
            };
        }

        public static implicit operator RegistrationForm(RegistrationFormRequest request)
        {
            return new RegistrationForm(request);
        }
    }
}

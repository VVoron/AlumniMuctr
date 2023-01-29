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
        public string? Faculty { get; set; }
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
        [Required]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool Subscription { get; set; } = false;
        public bool LiveOfAssociation { get; set; } = false;
        public bool FunSaturday { get; set; } = false;
        public bool DataProcessing { get; set; } = false;
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

        /*"Id",
                "ФИО",
                "ФИО в период обучения ",
                "Пол",
                "Дата рождения",
                "Факультет/кафедра",
                "Научный руководитель",
                "Год окончания университета",
                "Место проживания в настоящее время",
                "Место работы в настоящее время",
                "Занимаемая должность",
                "Значимые научные/профессиональные достижения",
                "Есть ли в Вашей семье выпускники РХТУ - МХТИ?",
                "Хобби, увлечения",
                "Загрузите Ваше выпускное фото или актуальное фото (при желании)",
                "Адресс электронной почты",
                "Контактный телефон",
                "Подписаться на рассылку новостной информации",
                "Хочу активно участвовать в жизни ассоциации",
                "Хочу выступить на 'Нескучной субботе'",
                "Согласие на обработку персональных данных"*/
        public string[] GetInfoForTable()
        {
            return new string[]
            {
                this.Id.ToString(),
                this.FCs,
                this.FCsгUniversity,
                this.Gender,
                this.Birthday.ToString(),
                this.Faculty,
                this.ScientificSupervisor,
                this.EndUniversityTime.ToString(),
                this.CurrentLivingPlace,
                this.CurrentWorkingPlace,
                this.CurrentPosition,
                this.SignificantAchievements,
                this.GraduatesOfMUCTRMHTI,
                this.Hobby,
                this.Photo,
                this.Email,
                this.Phone,
                this.Subscription.ToString(),
                this.LiveOfAssociation.ToString(),
                this.FunSaturday.ToString(),
                this.DataProcessing.ToString()
            };
        }
    }
}

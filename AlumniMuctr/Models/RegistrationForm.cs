using DocumentFormat.OpenXml.Office2010.Drawing;
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
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? Faculty { get; set; }
        public string? ScientificSupervisor { get; set; }
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
            Gender = request.Gender;
            Birthday = request.Birthday;
            Faculty = request.Faculty;
            ScientificSupervisor = request.ScientificSupervisor;
            EndUniversityTime = DateTime.ParseExact(
                    request.EndUniversityTime.ToString(),
                    "yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None
                );
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
            Photo = request.PhotoUrl;
            IsVerified = request.IsVerified;
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
                Id.ToString(),
                FCs,
                FCsгUniversity,
                Gender,
                Birthday.ToString(),
                Faculty,
                ScientificSupervisor,
                EndUniversityTime.ToString(),
                CurrentLivingPlace,
                CurrentWorkingPlace,
                CurrentPosition,
                SignificantAchievements,
                GraduatesOfMUCTRMHTI,
                Hobby,
                Photo,
                Email,
                Phone,
                Subscription.ToString(),
                LiveOfAssociation.ToString(),
                FunSaturday.ToString(),
                DataProcessing.ToString()
            };
        }

        public static bool IsAnyNullOrEmpty(object obj)
        {
            return !obj.GetType().GetProperties().All(x => x.GetValue(obj) != null);
        }

        public static implicit operator RegistrationForm(RegistrationFormRequest request)
        {
            return new RegistrationForm(request);
        }
    }
}

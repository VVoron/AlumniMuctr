using AlumniMuctr.Models;
using AlumniMuctr.Services.EmailService;
using EntityFrameworkCore.Triggered;

namespace AlumniMuctr.Services.DbTriggers
{
    public class RegistrationTrigger : IAfterSaveTrigger<RegistrationForm>
    {
        private readonly IEmailService _email;

        public RegistrationTrigger(IEmailService email)
        {
            _email = email;
        }

        public Task AfterSave(ITriggerContext<RegistrationForm> context, CancellationToken cancellationToken)
        {
            var email = new Email();
            email.To = context.Entity.Email;
            email.Subject = "Заявка на регистрацию в Ассоциации Выпускников РХТУ";

            if (context.Entity.IsVerified)
            {
                email.Body = "Поздравляем! Ваша заявка была одобрена!";
            }
            else if (context.ChangeType == ChangeType.Added)
            {
                email.Body = "Ваша заявка принята, заполните дополнитительные данные по ссылке ниже. Это ускорит одобрение вашего профиля." +
                    $"</br> <a href=\"https://localhost:7282/FullRegistration/{context.Entity.Id}\"";

            }
            else if (context.ChangeType == ChangeType.Modified)
            {
                email.Body = "Личные данные были успешно изменены.";
            }

            _email.SendEmail(email);

            return Task.CompletedTask;
        }
    }
}

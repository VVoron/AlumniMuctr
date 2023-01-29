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

            if (context.ChangeType == ChangeType.Added)
            {
                email.Subject = "Заявка на регистрацию в Ассоциации Выпускников РХТУ";
                email.Body = "Ваша заявка принята, заполните дополнитительные данные по ссылке ниже. Это ускорит одобрение вашего профиля.";
            } else if (context.ChangeType == ChangeType.Modified)
            {
                email.Subject = "Заявка на регистрацию в Ассоциации Выпускников РХТУ";
                email.Body = "Поздравляю, вы заполнилили ";
            }

            _email.SendEmail(email);

            return Task.CompletedTask;
        }
    }
}

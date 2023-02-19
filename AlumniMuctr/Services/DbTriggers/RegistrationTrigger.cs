using AlumniMuctr.Models;
using AlumniMuctr.Services.EmailService;
using EntityFrameworkCore.Triggered;
using System.IO;

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

            if (context.ChangeType == ChangeType.Added)
            {
                email.Body = File.ReadAllText(@"../AlumniMuctr/EmailTemplates/WelcomeTemplate.html");
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

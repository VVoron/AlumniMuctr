﻿using AlumniMuctr.Models;
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

        public async Task AfterSave(ITriggerContext<RegistrationForm> context, CancellationToken cancellationToken)
        {
            var email = new Email();
            email.To = context.Entity.Email;
            email.Subject = "Заявка на регистрацию в Ассоциации Выпускников РХТУ";

            if (context.ChangeType == ChangeType.Added)
            {
                email.Body = File.ReadAllText(@"../AlumniMuctr/EmailTemplates/WelcomeTemplate.html").Replace("{fullname}", context.Entity.FCs);
            }
            else if (context.ChangeType == ChangeType.Modified)
            {
                email.Body = "Личные данные были успешно изменены.";
            }
            else
            {
                return;
            }


            await _email.SendEmail(email);
        }
    }
}

using AlumniMuctr.Models;
using AlumniMuctr.Services.EmailService;
using EntityFrameworkCore.Triggered;

namespace AlumniMuctr.Services.DbTriggers
{
    public class SupportEmailTrgigger : IAfterSaveTrigger<Helper>
    {
        private readonly IEmailService _email;
        private readonly IConfiguration _configuration;

        public SupportEmailTrgigger(IEmailService email, IConfiguration configuration)
        {
            _email = email;
            _configuration = configuration;
        }

        public Task AfterSave(ITriggerContext<Helper> context, CancellationToken cancellationToken) 
        { 
            var email = new Email
            {
                To = _configuration.GetValue<string>("EmailService:Auth:User"),
                Subject = $"Вопрос #{context.Entity.Id} от {context.Entity.Name}. ({context.Entity.Email}) ",
                Body = context.Entity.Info
            };

            _email.SendEmail(email);

            return Task.CompletedTask;
        }
    }
}

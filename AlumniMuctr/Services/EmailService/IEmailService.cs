using AlumniMuctr.Models;

namespace AlumniMuctr.Services.EmailService
{
    public interface IEmailService
    {
        public Task SendEmail(Email request);
    }
}

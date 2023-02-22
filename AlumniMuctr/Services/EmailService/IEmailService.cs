using AlumniMuctr.Models;

namespace AlumniMuctr.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(Email request);
        public Task SendEmailAsync(Email request);
    }
}

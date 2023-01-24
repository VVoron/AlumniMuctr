using AlumniMuctr.Models;

namespace AlumniMuctr.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(Email request);
    }
}

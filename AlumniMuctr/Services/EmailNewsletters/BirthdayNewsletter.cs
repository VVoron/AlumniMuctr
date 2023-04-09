using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.EmailService;

namespace AlumniMuctr.Services.EmailNewsletters
{
    public class BirthdayNewsletter : IBirthdayNewsletter
    {
        private readonly IEmailService _email;
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public BirthdayNewsletter(IEmailService email, ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            _email = email;
            _dbContext = dbContext;
            _environment = environment;
        }

        public async Task SendBirthdayEmails()
        {
            var birthdayPeople = _dbContext.RegistrationForm
                .Where(x=>x.Subscription && x.IsVerified && x.Birthday == DateTime.Today && !x.FCs.Contains("(дубликат)"))
                .ToList();

            foreach (var p in birthdayPeople)
            {
                var fullName = p.FCs.Split(' ');

                var email = new Email();
                email.To = p.Email;
                email.Subject = "Поздравляем с Днем рождения!";
                email.Body = File.ReadAllText(_environment.ContentRootPath + @"/EmailTemplates/BirthdayTemplate.html")
                    .Replace("{name}", fullName[^2]).Replace("{sname}", fullName[^1]);

                await _email.SendEmailAsync(email);

                await Task.Delay(60000);
            }

        }
    }
}

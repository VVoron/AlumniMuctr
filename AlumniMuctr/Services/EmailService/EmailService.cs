using AlumniMuctr.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace AlumniMuctr.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;
        

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

            _host = _configuration.GetValue<string>("EmailService:Host");
            _port = _configuration.GetValue<int>("EmailService:Port");
            _user = _configuration.GetValue<string>("EmailService:Auth:User");
            _password = _configuration.GetValue<string>("EmailService:Auth:Pass");

        }

        public async Task SendEmail(Email request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_user));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_user, _password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}

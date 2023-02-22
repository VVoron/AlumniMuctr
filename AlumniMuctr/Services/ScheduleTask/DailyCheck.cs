using AlumniMuctr.Services.BackgroundService;
using AlumniMuctr.Services.EmailNewsletters;

namespace AlumniMuctr.Services.ScheduleTask
{
    public class DailyCheck : ScheduledProcessor
    {
        public DailyCheck(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "0 14 * * *";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var birthdayNewsletter = serviceProvider.GetRequiredService<BirthdayNewsletter>();


            await birthdayNewsletter.SendBirthdayEmails();

            //return Task.CompletedTask;
        }
    }
}

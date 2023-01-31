using AlumniMuctr.Data;
using AlumniMuctr.Services.DbTriggers;
using AlumniMuctr.Services.EmailService;
using AlumniMuctr.Services.HashService;
using AlumniMuctr.Services.SaveFileService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseTriggers(triggerOptions =>
    {
        triggerOptions.AddTrigger<SupportEmailTrgigger>();
        triggerOptions.AddTrigger<RegistrationTrigger>();
    });

});
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddScoped<ISaveFileService, SaveFileService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

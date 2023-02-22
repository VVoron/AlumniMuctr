using AlumniMuctr.Models;
using Microsoft.EntityFrameworkCore;

namespace AlumniMuctr.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        public DbSet<News> News { get; set; }
        public DbSet<Programms> Programms { get; set; }
        public DbSet<RegistrationForm> RegistrationForm { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Helper> Helper { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<FunSaturdayReg> FunSaturdayReg { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
    }
}

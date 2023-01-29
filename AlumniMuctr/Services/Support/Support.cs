using AlumniMuctr.Data;
using AlumniMuctr.Models;

namespace AlumniMuctr.Services.Support
{
    public class Support
    {
        public void AddedNewQuestion(Helper obj, ApplicationDbContext db)
        {
            db.Helper.Add(obj);
            db.SaveChanges();
        }
    }
}

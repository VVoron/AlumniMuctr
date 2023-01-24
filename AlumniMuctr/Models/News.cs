using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string BriefDescription { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public Categories Category { get; set; }
        public int CategoryId { get; set; }


        public News()
        {

        }

        public News(NewsRequest request)
        {
            Title = request.Title;
            BriefDescription = request.BriefDescription;
            Description = request.Description;
            CategoryId = request.CategoryId;
        }
    }
}

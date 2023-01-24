using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class Programms
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string BriefDescription { get; set; }
        public string Description { get; set; }
    }
}

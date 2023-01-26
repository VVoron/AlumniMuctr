using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //public IList<News> News { get; set; }
    }
}

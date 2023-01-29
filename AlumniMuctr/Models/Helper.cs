using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.Models
{
    public class Helper
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required] 
        public string Name { get;set; }
        [Required]
        public string Info { get; set; }
        public DateTime Created = DateTime.Now;
        public string[] GetInfoForTable()
        {
            return new string[]
            {
                Id.ToString(),
                Email.ToString(),
                Name.ToString(),
                Info,
                Created.ToString()
            };
        }
    }
}

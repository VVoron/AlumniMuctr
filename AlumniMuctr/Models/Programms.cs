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
        public string? Photo { get; set; }
        
        public Programms() { }

        public Programms(ProgrammsRequest request)
        {
            Id = request.Id;
            Title = request.Title;
            BriefDescription = request.BriefDescription;
            Description = request.Description;
            Photo = request.PhotoUrl;
        }

        public static implicit operator Programms(ProgrammsRequest request)
        {
            return new Programms(request);
        }
    }
}

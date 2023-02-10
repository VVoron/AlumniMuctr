namespace AlumniMuctr.Models
{
    public class ProgrammsRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BriefDescription { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }

        public ProgrammsRequest()
        {

        }

        public ProgrammsRequest(Programms programms)
        {
            Id = programms.Id;
            Title = programms.Title;
            BriefDescription = programms.BriefDescription;
            Description = programms.Description;
            PhotoUrl = programms.Photo;
        }
    }
}

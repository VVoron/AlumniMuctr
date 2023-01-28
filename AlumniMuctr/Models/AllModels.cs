namespace AlumniMuctr.Models
{
    public class AllModels
    {
        public IList<News> AllNews { get; set; }
        public IList<Programms> AllProgramms { get; set; }
        public RegistrationFormRequest Human { get; set; }
        public Helper Helper { get; set; }
    }
}

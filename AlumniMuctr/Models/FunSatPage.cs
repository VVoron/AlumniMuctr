namespace AlumniMuctr.Models
{
    public class FunSatPage
    {
        public IEnumerable<News> News { get; set;}
        public Helper? Helper { get; set; }
        public FunSaturdayReg FunSaturdayReg { get; set; }
    }
}

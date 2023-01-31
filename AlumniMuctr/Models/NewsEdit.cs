namespace AlumniMuctr.Models
{
    public class NewsEdit
    {
        public NewsRequest News { get; set; }
        public IList<Categories>? Categories { get; set; }

        public NewsEdit()
        {

        }

        public NewsEdit(NewsRequest request, IList<Categories> categories)
        {
            News = request;
            Categories = categories.ToList();
        }
    }
}

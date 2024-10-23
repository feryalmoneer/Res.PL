namespace Re.PL.Areas.Dashboard.ViewModels
{
    public class BlogVMForm
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string Discription { get; set; }
        public IFormFile Image { get; set; }
        public string? NameOfImg { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

namespace Re.PL.Areas.Dashboard.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }

        public string ImgName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

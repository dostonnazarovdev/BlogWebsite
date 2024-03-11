
namespace BlogWebsite.ViewModels
{
    public class PageVM
    {
        public int Id { get; set; }
        public string?  Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl  { get; set; }
        public IFormFile? Photo { get; set; }

    }
}

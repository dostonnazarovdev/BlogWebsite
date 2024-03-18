using BlogWebsite.Models;

namespace BlogWebsite.ViewModels
{
    public class HomeVM
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? PhotoUrl {  get; set; }
        public List<Post>?Posts { get; set; }
    }
}

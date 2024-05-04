namespace BlogWebsite.ViewModels
{
    public class BlogPostVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public DateTime CreateDate { get; set; }
        public string? PotoUrl { get; set; }
        public string? ShortDescription {  get; set; }
        public string? Description { get; set; }
    }
}

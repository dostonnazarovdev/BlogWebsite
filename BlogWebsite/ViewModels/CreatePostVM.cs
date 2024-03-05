using BlogWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogWebsite.ViewModels
{
    public class CreatePostVM
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? ApplicationUserId { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo { get; set; }
    }
}

using AspNetCoreHero.ToastNotification.Abstractions;
using BlogWebsite.Data;
using BlogWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        public INotyfService _notyfService { get; }

        public BlogController(ApplicationDbContext context,INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        [HttpGet]
        public IActionResult Post(string slug)
        {
            if (slug == "")
            {
                _notyfService.Error("Post not found!");
                 return View();
            }
            var post = _context.Posts!.Include(x=>x.ApplicationUser).FirstOrDefault(a=>a.Slug==slug);
            if (post == null)
            {
                _notyfService.Error("Post not found!");
                return View();
            }

            var vm = new BlogPostVM()
            {
                Id= post.Id,
                Title=post.Title,
                AuthorName=post.ApplicationUser!.LastName+" "+post.ApplicationUser.FirstName,
                CreateDate=post.CreatedDate,
                PotoUrl=post.PhotoUrl,
                Description=post.Description,
                ShortDescription=post.ShortDescription
            };

            return View(vm);
        }
    }
}

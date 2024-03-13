using BlogWebsite.Data;
using BlogWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Controllers
{
    public class PageController:Controller
    {
        private readonly ApplicationDbContext _context;
        public PageController(ApplicationDbContext context)
        {
           _context = context;
        }
        public async  Task<IActionResult> About()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            var vm = new PageVM()
            {
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                PhotoUrl = page.PhotoUrl
            };
            return View(vm);
        }

        public  async  Task<IActionResult> Contact()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");
            var vm = new PageVM()
            {
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                PhotoUrl = page.PhotoUrl
            };
            return View(vm);
        }

        public async Task<IActionResult> PrivacyPolicy()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "privacy");
            var vm = new PageVM()
            {
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                PhotoUrl = page.PhotoUrl
            };
            return View(vm);
        }
    }
}

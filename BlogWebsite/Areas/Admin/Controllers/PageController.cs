using AspNetCoreHero.ToastNotification.Abstractions;
using BlogWebsite.Data;
using BlogWebsite.Models;
using BlogWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class PageController : Controller
    {

        private readonly ApplicationDbContext _context;
        private INotyfService _notifySerivce { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PageController(ApplicationDbContext context,
                              INotyfService notyfService,
                              IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _notifySerivce = notyfService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> About ()
        {
            var aboutPage = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            var vm = new PageVM()
            {
                Id = aboutPage.Id,
                Title = aboutPage.Title,
                ShortDescription = aboutPage.ShortDescription,
                Description = aboutPage.Description,
                PhotoUrl = aboutPage.PhotoUrl
            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> About(PageVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
                var Page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");

            if (Page == null)
            {
                 _notifySerivce.Error("Page not found!");
                 return View();
            }
            Page.Title = vm.Title;
            Page.ShortDescription = vm.ShortDescription;
            Page.Description = vm.Description;

            if(vm.Photo!=null)
            {
                Page.PhotoUrl = UploadImage(vm.Photo);
            }
            await _context.SaveChangesAsync();
            _notifySerivce.Success("About page updated successfully");
            return RedirectToAction("About","Page",new {area="Admin"});
        }


        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            var aboutPage = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "About");
            var vm = new PageVM()
            {
                Id = aboutPage.Id,
                Title = aboutPage.Title,
                ShortDescription = aboutPage.ShortDescription,
                Description = aboutPage.Description,
                PhotoUrl = aboutPage.PhotoUrl
            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Contact(PageVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var Page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");

            if (Page == null)
            {
                _notifySerivce.Error("Page not found!");
                return View();
            }
            Page.Title = vm.Title;
            Page.ShortDescription = vm.ShortDescription;
            Page.Description = vm.Description;

            if (vm.Photo != null)
            {
                Page.PhotoUrl = UploadImage(vm.Photo);
            }
            await _context.SaveChangesAsync();
            _notifySerivce.Success("Contact page updated successfully");
            return RedirectToAction("Contact", "Page", new { area = "Admin" });
        }


        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            var aboutPage = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "Privacy");
            var vm = new PageVM()
            {
                Id = aboutPage.Id,
                Title = aboutPage.Title,
                ShortDescription = aboutPage.ShortDescription,
                Description = aboutPage.Description,
                PhotoUrl = aboutPage.PhotoUrl
            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Privacy(PageVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var Page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "privacy");

            if (Page == null)
            {
                _notifySerivce.Error("Page not found!");
                return View();
            }
            Page.Title = vm.Title;
            Page.ShortDescription = vm.ShortDescription;
            Page.Description = vm.Description;

            if (vm.Photo != null)
            {
                Page.PhotoUrl = UploadImage(vm.Photo);
            }
            await _context.SaveChangesAsync();
            _notifySerivce.Success("Privacy page updated successfully");
            return RedirectToAction("Privacy", "Page", new { area = "Admin" });
        }

        public string UploadImage(IFormFile file)
        {
            var uniqueFileName = "";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "photos");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}

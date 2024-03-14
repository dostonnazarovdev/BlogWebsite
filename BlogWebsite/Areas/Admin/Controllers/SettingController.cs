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
    [Authorize]
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private INotyfService _notifySerivce { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SettingController(ApplicationDbContext context,
                                 INotyfService notyfService,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _notifySerivce = notyfService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var setting = _context.Settings!.ToList();
            if (setting.Count > 0)
            {
                var vm = new SettingVM()
                {
                    Id = setting[0].Id,
                    SiteName = setting[0].SiteName,
                    Title = setting[0].Title,
                    ShortDescription = setting[0].ShortDescription,
                    PhotoUrl = setting[0].PhotoUrl,
                    FacebookUrl = setting[0].FacebookUrl,
                    GithubUrl = setting[0].GithubUrl,
                    TwitterUrl = setting[0].TwitterUrl
                };
                return View(vm);
            }
            var settings = new Setting()
            {
                SiteName = "Demo Name"
            };
            await _context.Settings!.AddAsync(settings);
            await _context.SaveChangesAsync();

            var createdSetting = _context.Settings!.ToList();
            var createdVM = new SettingVM()
            {
                Id = createdSetting[0].Id,
                SiteName = createdSetting[0].SiteName,
                Title = createdSetting[0].Title,
                ShortDescription = createdSetting[0].ShortDescription,
                PhotoUrl = createdSetting[0].PhotoUrl,
                FacebookUrl = createdSetting[0].FacebookUrl,
                GithubUrl = createdSetting[0].GithubUrl,
                TwitterUrl = createdSetting[0].TwitterUrl
            };
            return View(createdVM);

        }


        [HttpPost]
        public async Task<IActionResult> Index(SettingVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var setting = await _context.Settings!.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (setting == null)
            {
                _notifySerivce.Error("something went wrong!");
                return View(vm);
            }
            setting.SiteName = vm.SiteName;
            setting.Title = vm.Title;
            setting.ShortDescription = vm.ShortDescription;
            setting.PhotoUrl = vm.PhotoUrl;
            setting.FacebookUrl = vm.FacebookUrl;
            setting.GithubUrl = vm.GithubUrl;
            setting.TwitterUrl = vm.TwitterUrl;

            if(vm.Photo!= null)
            {
                setting.PhotoUrl = UploadImage(vm.Photo);
            }
            await _context.SaveChangesAsync();
             _notifySerivce.Success("Setting update succeffully");
            return RedirectToAction("Index", "Setting", new { area = "Admin" });
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

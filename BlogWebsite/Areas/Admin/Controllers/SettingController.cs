using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

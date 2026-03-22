using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoticeBoard.Web.Models;
using NoticeBoard.Web.Services;

namespace NoticeBoard.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementApiService _apiService;
        private static readonly Dictionary<string, List<string>> CategoryMap = new()
        {
            { "Побутова техніка", new List<string> { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" } },
            { "Комп'ютерна техніка", new List<string> { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" } },
            { "Смартфони", new List<string> { "Android смартфони", "iOS/Apple смартфони" } },
            { "Інше", new List<string> { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" } }
        };
        
        public AnnouncementsController(IAnnouncementApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string category)
        {
            var announcements = await _apiService.GetAllAsync();

            if (!string.IsNullOrEmpty(category))
            {
                announcements = announcements.Where(a => a.Category == category);
            }

            ViewBag.Categories = new List<string>
            {
                "Побутова техніка",
                "Комп'ютерна техніка",
                "Смартфони",
                "Інше"
            };
            ViewBag.SelectedCategory = category;

            return View(announcements);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewBag.CategoryMap = CategoryMap;
            return View(new AnnouncementCreateViewModel());
        }
 
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(AnnouncementCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryMap = CategoryMap;
                return View(model);
            }

            var success = await _apiService.CreateAsync(model);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Помилка при збереженні через API.");
            ViewBag.CategoryMap = CategoryMap;
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _apiService.GetByIdAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            var model = new AnnouncementUpdateViewModel
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Description = announcement.Description,
                Category = announcement.Category,
                SubCategory = announcement.SubCategory,
                Status = announcement.Status
            };

            ViewBag.CategoryMap = CategoryMap; 
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnnouncementUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryMap = CategoryMap;
                return View(model);
            }

            var success = await _apiService.UpdateAsync(id, model);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Помилка при оновленні через API.");
            ViewBag.CategoryMap = CategoryMap;
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _apiService.GetByIdAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _apiService.DeleteAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
